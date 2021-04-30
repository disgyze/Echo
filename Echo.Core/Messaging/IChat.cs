using Echo.Xmpp;

namespace Echo.Core.Messaging
{
    public interface IChat : IConversation
    {
        XmppAddress Address { get; }
    }
}