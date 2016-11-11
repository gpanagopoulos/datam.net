using System;
using Datam.Core.Services;

namespace Datam.Core.Commands
{
    public class MigrateCommand : ICommand
    {
        private readonly IPatchingService _patchingService;
        public MigrateCommand(IPatchingService patchingService)
        {
            _patchingService = patchingService;
        }
        public void Execute(Action<string> progressAction)
        {
            _patchingService.Migrate(progressAction);
        }
    }
}
