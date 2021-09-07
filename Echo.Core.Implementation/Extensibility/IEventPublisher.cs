using System;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility
{
    public interface IEventPublisher<TEventArgs> where TEventArgs : EventArgs
    {
        ValueTask<bool> PublishAsync(TEventArgs e);
    }
}