using System;
using System.Collections.Immutable;

namespace Echo.Foundation
{
    public sealed class Event<TEventArgs> : IEvent<TEventArgs> where TEventArgs : EventArgs
    {
        ImmutableArray<Subscription> subscriptionList = ImmutableArray<Subscription>.Empty;

        private sealed class Subscription : IDisposable
        {
            bool disposed = false;
            Func<TEventArgs, EventResult> handler = null;
            Action<Subscription> unsubscribe = null;

            public Subscription(Func<TEventArgs, EventResult> handler, Action<Subscription> unsubscribe)
            {
                this.handler = handler;
                this.unsubscribe = unsubscribe;
            }

            ~Subscription()
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
                if (!disposed)
                {
                    if (disposing)
                    {
                        unsubscribe(this);                        
                    }
                    disposed = true;
                }
            }

            public EventResult Invoke(TEventArgs e)
            {
                return handler.Invoke(e);
            }
        }

        public void Publish(TEventArgs e)
        {
            ImmutableArray<Subscription> temp = subscriptionList;

            for (int i = 0; i < temp.Length; i++)
            {
                if (temp[i].Invoke(e) == EventResult.Stop)
                {
                    break;
                }
            }
        }

        public IDisposable Subscribe(Func<TEventArgs, EventResult> handler)
        {
            var subscription = new Subscription(handler ?? throw new ArgumentNullException(nameof(handler)), self => RemoveSubscription(self));
            AddSubscription(subscription);
            return subscription;
        }

        private void AddSubscription(Subscription subscription)
        {
            ImmutableInterlocked.InterlockedExchange(ref subscriptionList, subscriptionList.Add(subscription));
        }

        private void RemoveSubscription(Subscription subscription)
        {
            ImmutableInterlocked.InterlockedExchange(ref subscriptionList, subscriptionList.Remove(subscription));
        }
    }
}