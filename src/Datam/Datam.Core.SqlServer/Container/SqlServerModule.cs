using Autofac;
using Datam.Core.DataAccess;
using Datam.Core.Services;
using Datam.Core.SqlServer.DataAccess;
using Datam.Core.SqlServer.Services;

namespace Datam.Core.SqlServer.Container
{
    public class SqlServerModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<SqlServerDatabaseUpdater>().As<IDatabaseUpdater>().SingleInstance();
            builder.RegisterType<SqlServerConnectionStringService>().As<IConnectionStringService>();
        }
    }
}
