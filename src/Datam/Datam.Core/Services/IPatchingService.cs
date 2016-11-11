using System;

namespace Datam.Core.Services
{
    public interface IPatchingService : IDisposable
    {
        void Migrate(Action<string> progressAction);
        void GetInfo(Action<string> progressAction);
    }
}
