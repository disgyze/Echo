using Echo.Core.Connections;

namespace Echo.Core.Messaging
{
    public sealed class Chat : Conversation
    {
        public ConversationState State { get; internal set; }

        public Chat(XmppAddress address, XmppConnectionService connection) : base(address, connection)
        {
        }
    }
}