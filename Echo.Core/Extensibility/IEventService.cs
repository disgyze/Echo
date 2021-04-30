using System;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility
{
    public interface IEventService
    {
        IDisposable RegisterEvent<TEventArgs>(Func<TEventArgs, Task<EventResult>> handler);
    }
}