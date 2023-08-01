using Echo.Core.Connections;

namespace Echo.Core.Messaging
{
    public readonly struct ConversationStateSentEventArgs
    {
        public XmppConnectionService Connection { get; }
        public Conversation Conversation { get; }
        public ConversationState State { get; }

        public ConversationStateSentEventArgs(XmppConnectionService connection, Conversation conversation, ConversationState state)
        {
            Connection = connection;
            Conversation = conversation;
            State = state;
        }
    }
}