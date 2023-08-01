using Echo.Core.Connections;

namespace Echo.Core.Messaging
{
    public readonly struct ConversationStateReceivedEventArgs
    {
        public XmppConnectionService Connection { get; }
        public Conversation Conversation { get; }
        public ConversationState State { get; }

        public ConversationStateReceivedEventArgs(XmppConnectionService connection, Conversation conversation, ConversationState state)
        {
            Connection = connection;
            Conversation = conversation;
            State = state;
        }
    }
}