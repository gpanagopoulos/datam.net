using System.Configuration;
using Datam.Core.Providers;
using Datam.Core.Services;

namespace Datam.Core.SqlServer.Services
{
    public class SqlServerConnectionStringService : IConnectionStringService
    {
        private readonly IApplicationContextProvider _appContextProvider;
        public SqlServerConnectionStringService(IApplicationContextProvider appContextProvider)
        {
            _appContextProvider = appContextProvider;
        }

        public string BuildConnectionString()
        {
            var appContext = _appContextProvider.GetApplicationContext();
            if (string.IsNullOrWhiteSpace(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                return string.Format("Server={0};Database={1};User Id={2};Password={3}", appContext.Server, appContext.Database, appContext.Username, appContext.Password);
            }
            return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
    }
}
