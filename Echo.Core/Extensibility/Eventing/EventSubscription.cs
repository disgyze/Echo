using System;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility.Eventing
{
    internal sealed class EventSubscription<TEventArgs> : IDisposable where TEventArgs : struct
    {
        bool disposed = false;
        Func<TEventArgs, ValueTask<EventResult>> handler;
        Action<EventSubscription<TEventArgs>> unsubscribe;

        public EventSubscription(Func<TEventArgs, ValueTask<EventResult>> handler, Action<EventSubscription<TEventArgs>> unsubscribe)
        {
            this.handler = handler;
            this.unsubscribe = unsubscribe;
        }

        ~EventSubscription()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (disposed)
            {
                return;
            }

            if (disposing)
            {
                unsubscribe(this);
            }

            disposed = true;
        }

        public ValueTask<EventResult> Invoke(TEventArgs e)
        {
            return handler.Invoke(e);
        }
    }
}