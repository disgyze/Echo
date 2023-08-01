using System.Collections.Immutable;
using System.Threading.Tasks;

namespace Echo.Core.Extensibility.Eventing
{
    internal sealed class EventChannelManager
    {
        ImmutableArray<EventChannel> channelCollection = ImmutableArray<EventChannel>.Empty;

        public async ValueTask<EventResult> PublishAsync<TEventArgs>(TEventArgs e) where TEventArgs : struct
        {
            foreach (var channel in channelCollection)
            {
                if (channel.GetEvent<TEventArgs>() is Event<TEventArgs> @event)
                {
                    if (await @event.PublishAsync(e).ConfigureAwait(false) == EventResult.Stop)
                    {
                        return EventResult.Stop;
                    }
                }
            }
            return EventResult.Continue;
        }

        public bool AddApplicationChannel(EventChannel channel)
        {
            return ImmutableInterlocked.Update(ref channelCollection, static (collection, channel) => collection.Contains(channel) ? collection : collection.Add(channel), channel);
        }

        public bool AddPluginChannel(EventChannel channel)
        {
            return ImmutableInterlocked.Update(ref channelCollection, static (collection, channel) => collection.Contains(channel) ? collection : collection.Insert(collection.Length - 1, channel), channel);
        }

        public bool Remove(EventChannel channel)
        {
            return ImmutableInterlocked.Update(ref channelCollection, static (collection, channel) => collection.Remove(channel), channel);
        }

        public bool MoveUp(EventChannel channel)
        {
            return ImmutableInterlocked.Update(ref channelCollection, static (collection, channel1) =>
            {
                if (collection.Length < 3)
                {
                    return collection;
                }

                var channelIndex1 = collection.IndexOf(channel1);

                if (channelIndex1 <= 0 || channelIndex1 == collection.Length - 1)
                {
                    return collection;
                }

                var channelIndex2 = channelIndex1 - 1;
                var channel2 = collection[channelIndex2];

                return collection.SetItem(channelIndex1, channel2)
                                 .SetItem(channelIndex2, channel1);
            }, channel);
        }

        public bool MoveDown(EventChannel channel)
        {
            return ImmutableInterlocked.Update(ref channelCollection, static (collection, channel1) =>
            {
                if (collection.Length < 3)
                {
                    return collection;
                }

                var channelIndex1 = collection.IndexOf(channel1);

                if (channelIndex1 < 0 || channelIndex1 == collection.Length - 1)
                {
                    return collection;
                }

                var channelIndex2 = channelIndex1 + 1;

                if (channelIndex2 == collection.Length - 1)
                {
                    return collection;
                }

                var channel2 = collection[channelIndex2];

                return collection.SetItem(channelIndex1, channel2)
                                 .SetItem(channelIndex2, channel1);

            }, channel);
        }
    }
}