using System;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility
{
    public interface ICommandService
    {
        IDisposable RegisterCommand(string name, Func<CommandArgs, Task<CommandResult>> handler);
    }
}