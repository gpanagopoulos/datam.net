using System;
using System.Collections.Generic;
using Datam.Core.DataAccess;
using Datam.Core.Model;
using Datam.Core.ScriptReader;

namespace Datam.Core.Services
{
    public class PatchingService : IPatchingService
    {
        private bool _disposed;
        private readonly IDatabaseUpdater _databaseUpdater;
        private readonly IScriptReader _patchesReader;

        public PatchingService(IDatabaseUpdater databaseUpdater, IScriptReader patchesReader)
        {
            _databaseUpdater = databaseUpdater;
            _patchesReader = patchesReader;
        }

        #region IPatchingService

        public void Migrate(Action<string> progressAction)
        {
            string filenameBeingApplied = string.Empty;

            try
            {
                //Check if has run once
                bool hasInit = _databaseUpdater.CheckTableExists("migrations", "Scripts");
                if (!hasInit)
                {
                    _databaseUpdater.Initialise();
                    progressAction("Initialised database");
                }

                foreach (var script in _patchesReader.GetScripts())
                {
                    filenameBeingApplied = _patchesReader.GetScriptName(script);

                    if (!_databaseUpdater.HasScriptExecuted(filenameBeingApplied))
                    {
                        var sqlScript = _patchesReader.ReadAllScriptText(script);
                        _databaseUpdater.ExecuteScript(sqlScript);
                        bool executed = _databaseUpdater.UpgradeVersion(filenameBeingApplied);

                        if (!executed)
                            throw new ApplicationException("Failed to upgrade db version.");

                        progressAction("Applied: " + filenameBeingApplied);
                    }
                }

                _databaseUpdater.Commit();
            }
            catch (Exception ex)
            {
                _databaseUpdater.Rollback();
                progressAction(string.Format("Failed to apply: {0}. Message:{1}, StackTrace: {2}. Rolling back all changes...",
                    filenameBeingApplied, ex.Message, ex.StackTrace));
            }
            finally
            {
                _databaseUpdater.Close();
            }
        }

        public void GetInfo(Action<string> progressStatusAction)
        {
            IEnumerable<Migration> migrations = _databaseUpdater.GetMigrationInfo();

            progressStatusAction(string.Format("{0} | {1}", "Filename", "DatetimeApplied"));
            foreach (var migration in migrations)
            {
                progressStatusAction(string.Format("{0} | {1}", migration.Filename, migration.DateTimeApplied));
            }
        }

        #endregion IPatchingService

        #region IDisposable

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _databaseUpdater.Dispose();
                }

                // clean up unmanaged resources here..

                // Note disposing has been done.
                _disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion IDisposable
    }
}
