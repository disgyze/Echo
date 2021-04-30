using System;

namespace Echo.Foundation
{
    public interface IEvent<TEventArgs> : IEventPublisher<TEventArgs>, IEventSubscriber<TEventArgs> where TEventArgs : EventArgs
    {
    }
}