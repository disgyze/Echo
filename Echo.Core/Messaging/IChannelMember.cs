namespace Echo.Core.Messaging
{
    public interface IChannelMember
    {
        IChannel Channel { get; }
        XmppAddress Address { get; }
        string Nick { get; }
    }
}