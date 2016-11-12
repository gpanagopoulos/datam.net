﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Datam.Core.Helpers;
using Datam.Core.DataAccess;
using Datam.Core.Model;
using Datam.Core.Services;

namespace Datam.Core.SqlServer.DataAccess
{
    public class SqlServerDatabaseUpdater : IDatabaseUpdater
    {
        private bool _disposed;
        private readonly SqlConnection _connection;
        private readonly SqlTransaction _transaction;

        public SqlServerDatabaseUpdater(IConnectionStringService service)
        {
            _connection = new SqlConnection() {ConnectionString = service.BuildConnectionString()};
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public bool CheckTableExists(string schema, string table)
        {
            string cmdText = @"IF EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='" + table + "' AND TABLE_SCHEMA = '" + schema + "') SELECT 1 ELSE SELECT 0";
            SqlCommand command = _connection.CreateCommand();
            command.CommandText = cmdText;
            command.Transaction = _transaction;
            var ret = command.ExecuteScalar();

            if (ret is int)
                return (int) ret == 1;
            throw new ApplicationException("CheckTableExists: Incorrect return value returned from query - Expected 0 or 1");
        }

        public bool HasInitialised()
        {
            return CheckTableExists("migrations", "scripts_history");
        }

        public IEnumerable<Migration> GetMigrationInfo()
        {
            List<Migration> migrations = new List<Migration>();
            string sqlText = @"EXEC [migrations].[GetMigrationInfo]";
            SqlCommand command = _connection.CreateCommand();
            command.CommandText = sqlText;
            using (var reader = command.ExecuteReader())
            {
                while (reader.NextResult())
                {
                    migrations.Add(new Migration()
                    {
                        Filename = reader.GetString(0),
                        DateTimeApplied = reader.GetDateTime(1)
                    });
                }
            }

            return migrations;
        }

        public void Initialise()
        {
            ExecuteScript(EmbeddedAppScripts.CREATE_SCHEMA_MIGRATIONS);
            ExecuteScript(EmbeddedAppScripts.CREATE_TABLE_SCRIPT_HISTORY);
            ExecuteScript(EmbeddedAppScripts.CREATE_GET_MIGRATION_INFO);
            ExecuteScript(EmbeddedAppScripts.CREATE_HAS_SCRIPT_EXECUTED);
            ExecuteScript(EmbeddedAppScripts.CREATE_UPGRADE_VERSION);
        }

        public void ExecuteScript(string sqlScript)
        {
            string[] commandTexts = sqlScript.SplitSqlStatementsOnGo().ToArray();
            foreach (string commandText in commandTexts)
            {
                var applyCommand = _connection.CreateCommand();
                applyCommand.CommandText = commandText;
                applyCommand.Transaction = _transaction;
                applyCommand.ExecuteNonQuery();
            }
        }

        public bool UpgradeVersion(string scriptName)
        {
            string cmdText = string.Format(@"EXEC [migrations].[UpgradeVersion] N'{0}'", scriptName);

            SqlCommand newUpdateCommand = _connection.CreateCommand();
            newUpdateCommand.CommandText = cmdText;
            newUpdateCommand.Transaction = _transaction;

            int rowsAffected = newUpdateCommand.ExecuteNonQuery();
            return rowsAffected == 1;
        }

        public bool HasScriptExecuted(string name)
        {
            string cmdText = string.Format(@"EXEC [migrations].[HasScriptExecuted] N'{0}'", name);

            var command = _connection.CreateCommand();
            command.CommandText = cmdText;
            command.Transaction = _transaction;

            var ret = command.ExecuteScalar();

            if (ret is int)
                return (int) ret == 1;
            throw new ApplicationException(nameof(HasScriptExecuted) +
                                           "Incorrect return value returned from query - Expected 0 or 1");
        }

        public void Commit()
        {
            _transaction.Commit();
        }

        public void Rollback()
        {
            _transaction.Rollback();
        }

        public void Close()
        {
            _connection.Close();
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _transaction.Dispose();
                    _connection.Dispose();
                }

                // Clean up unmanaged resources here..

                // Note disposing has been done.
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
