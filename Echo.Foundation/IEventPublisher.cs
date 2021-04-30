using System;

namespace Echo.Foundation
{
    public interface IEventPublisher<TEventArgs> where TEventArgs : EventArgs
    {
        void Publish(TEventArgs e);
    }
}