using System;
using Datam.Core.Services;

namespace Datam.Core.Commands
{
    public class InfoCommand : ICommand
    {
        private readonly IPatchingService _patchingService;

        public InfoCommand(IPatchingService patchingService)
        {
            _patchingService = patchingService;
        }

        public void Execute(Action<string> progressAction)
        {
            _patchingService.GetInfo(progressAction);
        }
    }
}
