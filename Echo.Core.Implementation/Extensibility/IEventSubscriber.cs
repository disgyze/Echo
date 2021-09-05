using System;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility
{
    public interface IEventSubscriber<TEventArgs> where TEventArgs : EventArgs
    {
        IDisposable Subscribe(Func<TEventArgs, ValueTask<EventResult>> handler);
    }
}