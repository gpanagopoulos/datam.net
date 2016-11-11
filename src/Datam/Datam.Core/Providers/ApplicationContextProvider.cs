using Datam.Core.Model;
using Datam.Core.Services;

namespace Datam.Core.Providers
{
    public class ApplicationContextProvider : IApplicationContextProvider
    {
        private readonly IConfigService _configService;
        private readonly IOptionsParserService _optionsParserService;
        public ApplicationContextProvider(IConfigService configService, IOptionsParserService optionsParserService)
        {
            _configService = configService;
            _optionsParserService = optionsParserService;
        }

        public ApplicationContext GetApplicationContext()
        {
            var options = _optionsParserService.GetOptions();
            return new ApplicationContext()
            {
                Database = options.Database,
                Password = options.Password,
                PatchesFolder = _configService.GetPatchesFolder(),
                PatchesRegex = _configService.GetPatchesRegex(),
                Server = options.Server,
                Username = options.Username,
                OperationType = options.OperationType,
                Port = options.Port
            };
        }
    }
}
