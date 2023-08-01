using System;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility.Eventing
{
    public interface IEventSubscriber<TEventArgs> where TEventArgs : struct
    {
        IDisposable Subscribe(Func<TEventArgs, ValueTask<EventResult>> handler);
    }
}