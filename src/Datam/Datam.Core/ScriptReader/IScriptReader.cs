using System.Collections.Generic;

namespace Datam.Core.ScriptReader
{
    public interface IScriptReader
    {
        IEnumerable<string> GetScripts();
        string GetScriptName(string script);
        string ReadAllScriptText(string script);
    }
}
