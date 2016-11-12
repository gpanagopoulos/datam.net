using Autofac;
using Datam.Core.DataAccess;
using Datam.Core.MySql.DataAccess;
using Datam.Core.MySql.Services;
using Datam.Core.Services;

namespace Datam.Core.MySql.Container
{
    public class MySqlModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<MySqlDatabaseUpdater>().As<IDatabaseUpdater>().SingleInstance();
            builder.RegisterType<MySqlConnectionStringService>().As<IConnectionStringService>();
        }
    }
}
