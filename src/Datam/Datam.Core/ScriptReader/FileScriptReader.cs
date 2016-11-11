using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Datam.Core.Helpers;
using Datam.Core.Services;

namespace Datam.Core.ScriptReader
{
    public class FileScriptReader : IScriptReader
    {
        private readonly IConfigService _configService;
        public FileScriptReader(IConfigService configService)
        {
            _configService = configService;
        }

        public IEnumerable<string> GetScripts()
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;//assemblyLocation does not work with nunit
            string path = PathHelper.GetRelativeLocation(codeBase, _configService.GetPatchesFolder());

            return Directory.GetFiles(path, _configService.GetPatchesRegex());
        }

        public string GetScriptName(string script)
        {
            return Path.GetFileName(script);
        }

        public string ReadAllScriptText(string script)
        {
            return File.ReadAllText(script);
        }
    }
}
