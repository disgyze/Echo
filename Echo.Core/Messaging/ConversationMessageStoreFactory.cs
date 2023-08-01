using Echo.Core.Connections;

namespace Echo.Core.Messaging
{
    public abstract class ConversationMessageStoreFactory
    {
        public abstract ConversationMessageStore Create(XmppConnectionService connection);
    }
}