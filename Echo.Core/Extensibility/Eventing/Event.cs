using System;
using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility.Eventing
{
    internal sealed class Event<TEventArgs> : IEventPublisher<TEventArgs>, IEventSubscriber<TEventArgs> where TEventArgs : struct
    {
        ImmutableArray<EventSubscription<TEventArgs>> subscriptionCollection = ImmutableArray<EventSubscription<TEventArgs>>.Empty;

        public async ValueTask<EventResult> PublishAsync(TEventArgs e)
        {
            foreach (var subscription in subscriptionCollection)
            {
                // TODO What if an exception is thrown?
                if (await subscription.Invoke(e).ConfigureAwait(false) == EventResult.Stop)
                {
                    return EventResult.Stop;
                }
            }
            return EventResult.Continue;
        }

        public IDisposable Subscribe(Func<TEventArgs, ValueTask<EventResult>> handler)
        {
            var subscription = new EventSubscription<TEventArgs>(handler ?? throw new ArgumentNullException(nameof(handler)), RemoveSubscription);
            AddSubscription(subscription);
            return subscription;
        }

        private void AddSubscription(EventSubscription<TEventArgs> subscription)
        {
            ImmutableInterlocked.Update(ref subscriptionCollection, static (collection, subscription) => collection.Add(subscription), subscription);
        }

        private void RemoveSubscription(EventSubscription<TEventArgs> subscription)
        {
            ImmutableInterlocked.Update(ref subscriptionCollection, static (collection, subscription) => collection.Remove(subscription), subscription);
        }
    }
}