using System;

namespace Echo.Core.Extensibility
{
    public interface IEvent<TEventArgs> : IEventPublisher<TEventArgs>, IEventSubscriber<TEventArgs> where TEventArgs : EventArgs
    {
    }
}