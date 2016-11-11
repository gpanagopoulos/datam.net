using Datam.Core.Model;

namespace Datam.Core.Services
{
    public interface IOptionsParserService
    {
        Options ParseOptions(string[] args);
        Options GetOptions();
    }
}
