using System;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility
{
    public interface IEventService
    {
        IDisposable RegisterEvent<TEventArgs>(Func<TEventArgs, ValueTask<EventResult>> handler);
    }
}