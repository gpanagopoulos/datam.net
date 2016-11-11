using System.Configuration;
namespace Datam.Core.Services
{
    public class AppConfigService : IConfigService
    {
        public string GetPatchesFolder()
        {
            return ConfigurationManager.AppSettings.Get("PatchesFolder");
        }

        public string GetPatchesRegex()
        {
            return ConfigurationManager.AppSettings.Get("PatchesRegex");
        }
    }
}
