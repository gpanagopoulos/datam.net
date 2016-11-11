using System;

namespace Datam.Core.Commands
{
    public interface ICommand
    {
        void Execute(Action<string> progressAction);
    }
}
