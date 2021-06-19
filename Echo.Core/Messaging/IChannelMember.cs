namespace Echo.Core.Messaging
{
    public interface IChannelMember
    {
        IChannel Channel { get; }
        XmppUri Address { get; }
        string Nick { get; }
    }
}