using System.Text;
using CommandLine;

namespace Datam.Core.Model
{
    public class Options
    {
        [Option('s', "server", Required = false, HelpText = "DB Server name to connect to. Required if connection string is not provided in configuration.")]
        public string Server { get; set; }

        [Option('d', "database", HelpText = "Database name to connect to. Required if connection string is not provided in configuration.")]
        public string Database { get; set; }

        [Option('o', "operation", Required = true, HelpText = "Operation to perform (i.e Migrate, Clean, Info)")]
        public OperationType OperationType { get; set; }

        [Option('u', "username", Required = false, HelpText = "Username. Required if connection string is not provided in configuration.")]
        public string Username { get; set; }

        [Option('p', "password", Required = false, HelpText = "Password. Required if connection string is not provided in configuration.")]
        public string Password { get; set; }

        [Option('t', "port", Required = false, HelpText = "Port. Required if connection string is not provided in configuration.")]
        public string Port { get; set; }

        [HelpOption(HelpText = "Display this help screen.")]
        public string GetUsage()
        {
            var usage = new StringBuilder();
            usage.AppendLine("Database Patching Application");
            usage.AppendLine("Read user manual for usage instructions...");
            return usage.ToString();
        }
    }
}
