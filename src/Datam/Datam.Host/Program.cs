using System;
using System.Configuration;
using Autofac;
using Datam.Core.Commands;
using Datam.Core.Container;
using Datam.Core.Model;
using Datam.Core.Services;
using Datam.Core.SqlServer.Container;

namespace Datam.Host
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CoreModule());
            switch (ConfigurationManager.AppSettings["DBType"])
            {
                case "SQLServer":
                    builder.RegisterModule(new SqlServerModule());
                    break;
                default:
                    throw new ApplicationException("Wrong DBType set in Application Configuration. Please change to SQLServer.");
            }
            
            using (var container = builder.Build())
            {
                var optionsParser = container.Resolve<IOptionsParserService>();
                var options = optionsParser.ParseOptions(args);
                
                var command = container.ResolveNamed<ICommand>(options.OperationType.ToString());
                command.Execute((message) =>
                {
                    Console.WriteLine(message);
                });
            }
            Console.ReadLine();
        }
    }
}
