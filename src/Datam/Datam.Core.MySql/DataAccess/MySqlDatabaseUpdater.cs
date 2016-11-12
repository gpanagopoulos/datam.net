using System;
using System.Collections.Generic;
using System.Linq;
using Datam.Core.DataAccess;
using Datam.Core.Helpers;
using Datam.Core.Model;
using Datam.Core.Services;
using MySql.Data.MySqlClient;

namespace Datam.Core.MySql.DataAccess
{
    public class MySqlDatabaseUpdater : IDatabaseUpdater
    {
        private bool _disposed;
        private readonly MySqlConnection _connection;
        private readonly MySqlTransaction _transaction;

        public MySqlDatabaseUpdater(IConnectionStringService service)
        {
            _connection = new MySqlConnection() { ConnectionString = service.BuildConnectionString() };
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }

        public bool CheckTableExists(string schema, string table)
        {
            string cmdText = @"SELECT EXISTS(SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME='" + table + "' AND TABLE_SCHEMA = '" + schema + "')";
            MySqlCommand command = _connection.CreateCommand();
            command.CommandText = cmdText;
            command.Transaction = _transaction;
            var ret = command.ExecuteScalar();

            if (ret is long)
                return (long)ret == 1;
            throw new ApplicationException("CheckTableExists: Incorrect return value returned from query - Expected 0 or 1");
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
            string cmdText = string.Format(@"CALL upgrade_version ('{0}')", scriptName);

            MySqlCommand newUpdateCommand = _connection.CreateCommand();
            newUpdateCommand.CommandText = cmdText;
            newUpdateCommand.Transaction = _transaction;

            int rowsAffected = newUpdateCommand.ExecuteNonQuery();
            return rowsAffected == 1;
        }

        public bool HasInitialised()
        {
            return CheckTableExists(_connection.Database, "scripts_history");
        }

        public void Initialise()
        {
            ExecuteScript(EmbeddedAppScripts.CREATE_TABLE_SCRIPT_HISTORY);
            ExecuteScript(EmbeddedAppScripts.CREATE_GET_MIGRATION_INFO);
            ExecuteScript(EmbeddedAppScripts.CREATE_HAS_SCRIPT_EXECUTED);
            ExecuteScript(EmbeddedAppScripts.CREATE_UPGRADE_VERSION);
        }

        public IEnumerable<Migration> GetMigrationInfo()
        {
            List<Migration> migrations = new List<Migration>();
            string sqlText = @"CALL get_migration_info();";
            MySqlCommand command = _connection.CreateCommand();
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

        public bool HasScriptExecuted(string name)
        {
            string cmdText = string.Format(@"CALL has_script_executed ('{0}');", name);

            var command = _connection.CreateCommand();
            command.CommandText = cmdText;
            command.Transaction = _transaction;

            var ret = command.ExecuteScalar();

            if (ret is long)
                return (long)ret == 1;
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
