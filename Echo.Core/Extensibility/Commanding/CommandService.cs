using System;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility.Commanding
{
    public abstract class CommandService
    {
        public abstract IDisposable RegisterCommand(string name, Func<CommandArgs, ValueTask<CommandResult>> handler);
    }
}