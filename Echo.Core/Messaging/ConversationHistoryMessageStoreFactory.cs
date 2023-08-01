using Echo.Core.Connections;

namespace Echo.Core.Messaging
{
    public abstract class ConversationHistoryMessageStoreFactory
    {
        public abstract ConversationHistoryMessageStore Create(XmppConnectionService connection);
    }
}