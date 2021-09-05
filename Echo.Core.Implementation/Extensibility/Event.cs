using System;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility
{
    public sealed class Event<TEventArgs> : IEvent<TEventArgs> where TEventArgs : EventArgs
    {
        ImmutableArray<Subscription> subscriptionList = ImmutableArray<Subscription>.Empty;

        private sealed class Subscription : IDisposable
        {
            bool disposed = false;
            Func<TEventArgs, ValueTask<EventResult>> handler = null!;
            Action<Subscription> unsubscribe = null!;

            public Subscription(Func<TEventArgs, ValueTask<EventResult>> handler, Action<Subscription> unsubscribe)
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

        public async ValueTask<bool> PublishAsync(TEventArgs e)
        {
            var temp = subscriptionList;

            for (int i = 0; i < temp.Length; i++)
            {
                var task = temp[i].Invoke(e);
                var result = task.IsCompleted ? task.GetAwaiter().GetResult() : await task.AsTask();

                if (result == EventResult.Stop)
                {
                    return false;
                }
            }

            return true;
        }

        public IDisposable Subscribe(Func<TEventArgs, ValueTask<EventResult>> handler)
        {
            var subscription = new Subscription(handler ?? throw new ArgumentNullException(nameof(handler)), RemoveSubscription);
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