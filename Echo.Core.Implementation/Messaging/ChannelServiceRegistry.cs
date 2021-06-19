using System.Collections.Immutable;

namespace Echo.Core.Messaging
{
    public sealed class ChannelServiceRegistry : IChannelServiceRegistry
    {
        ImmutableDictionary<XmppUri, ChannelServiceKind?> addressMap = ImmutableDictionary<XmppUri, ChannelServiceKind?>.Empty;

        public bool TryGet(XmppUri address, out ChannelServiceKind? kind)
        {
            return addressMap.TryGetValue(address, out kind);
        }

        public bool TryAdd(XmppUri address, ChannelServiceKind kind)
        {
            return ImmutableInterlocked.TryAdd(ref addressMap, address, kind);
        }

        public bool TryRemove(XmppUri address)
        {
            return ImmutableInterlocked.TryRemove(ref addressMap, address, out _);
        }
    }
}