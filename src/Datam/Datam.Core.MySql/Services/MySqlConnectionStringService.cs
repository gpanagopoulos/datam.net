using System.Configuration;
using Datam.Core.Providers;
using Datam.Core.Services;

namespace Datam.Core.MySql.Services
{
    public class MySqlConnectionStringService : IConnectionStringService
    {
        private readonly IApplicationContextProvider _appContextProvider;

        public MySqlConnectionStringService(IApplicationContextProvider appContextProvider)
        {
            _appContextProvider = appContextProvider;
        }

        public string BuildConnectionString()
        {
            var appContext = _appContextProvider.GetApplicationContext();
            if (string.IsNullOrWhiteSpace(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
            {
                return string.Format("server={0};port={1};database={2};uid={3};pwd={4}", appContext.Server,
                    appContext.Database, appContext.Port, appContext.Username, appContext.Password);
            }
            return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
    }
}
