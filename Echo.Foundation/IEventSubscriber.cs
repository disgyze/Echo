using System;

namespace Echo.Foundation
{
    public interface IEventSubscriber<TEventArgs> where TEventArgs : EventArgs
    {
        IDisposable Subscribe(Func<TEventArgs, EventResult> handler);
    }
}