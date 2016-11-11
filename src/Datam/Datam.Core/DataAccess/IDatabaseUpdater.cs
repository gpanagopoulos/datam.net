using System;
using System.Collections.Generic;
using Datam.Core.Model;

namespace Datam.Core.DataAccess
{
    public interface IDatabaseUpdater : IDisposable
    {
        bool CheckTableExists(string schema, string table);
        bool HasScriptExecuted(string name);
        void ExecuteScript(string sqlScript);
        bool UpgradeVersion(string scriptName);
        void Initialise();
        IEnumerable<Migration> GetMigrationInfo();


        void Commit();
        void Rollback();
        void Close();
    }
}
