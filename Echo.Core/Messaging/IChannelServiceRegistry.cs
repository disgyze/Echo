namespace Echo.Core.Messaging
{
    public interface IChannelServiceRegistry
    {
        bool TryGet(XmppUri address, out ChannelServiceKind? kind);
        bool TryAdd(XmppUri address, ChannelServiceKind kind);
        bool TryRemove(XmppUri address);
    }
}