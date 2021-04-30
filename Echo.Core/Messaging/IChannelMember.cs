using Echo.Xmpp;

namespace Echo.Core.Messaging
{
    public interface IChannelMember
    {
        IChannel Room { get; }
        XmppAddress Address { get; }
        string Nickname { get; }
    }
}