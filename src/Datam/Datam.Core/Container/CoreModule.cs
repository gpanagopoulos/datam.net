using System.Configuration;
using Autofac;
using Datam.Core.Commands;
using Datam.Core.Model;
using Datam.Core.Providers;
using Datam.Core.ScriptReader;
using Datam.Core.Services;

namespace Datam.Core.Container
{
    public class CoreModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AppConfigService>().As<IConfigService>();
            builder.RegisterType<OptionsParserService>().As<IOptionsParserService>().SingleInstance();
            builder.RegisterType<FileScriptReader>().As<IScriptReader>();
            builder.RegisterType<PatchingService>().As<IPatchingService>();
            builder.RegisterType<ApplicationContextProvider>().As<IApplicationContextProvider>();
            builder.RegisterType<InfoCommand>().Named<ICommand>(OperationType.Info.ToString());
            builder.RegisterType<MigrateCommand>().Named<ICommand>(OperationType.Migrate.ToString());
        }
    }
}
