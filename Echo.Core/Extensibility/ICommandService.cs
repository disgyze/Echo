using System;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility
{
    public interface ICommandService
    {
        IDisposable RegisterCommand(string name, Func<CommandArgs, ValueTask<CommandResult>> handler);
    }
}