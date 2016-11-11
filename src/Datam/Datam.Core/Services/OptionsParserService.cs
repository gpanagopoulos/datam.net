using System;
using Datam.Core.Model;

namespace Datam.Core.Services
{
    public class OptionsParserService : IOptionsParserService
    {
        private Options _options = null;
        public Options ParseOptions(string[] args)
        {
            if (_options == null)
                _options = new Options();
            if (!CommandLine.Parser.Default.ParseArguments(args, _options))
            {
                string error = "Error: Incorrect command line arguments";
                throw new ApplicationException(error);
            }

            return _options;
        }

        public Options GetOptions()
        {
            if(_options == null)
                throw new ApplicationException("Error: No Options have been parsed.");
            return _options;
        }
    }
}
