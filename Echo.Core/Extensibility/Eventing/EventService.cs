using System;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility.Eventing
{
    public sealed class EventService
    {
        EventChannel eventChannel;

        internal EventService(EventChannel eventChannel)
        {
            this.eventChannel = eventChannel ?? throw new ArgumentNullException(nameof(eventChannel));
        }

        public IDisposable RegisterEvent<TEventArgs>(Func<TEventArgs, ValueTask<EventResult>> handler) where TEventArgs : struct
        {
            return eventChannel.GetEvent<TEventArgs>().Subscribe(handler);
        }
    }
}