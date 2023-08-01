namespace Echo.Core.Messaging
{
    public readonly struct ConversationHistoryMessageReceivedEventArgs
    {
        public ConversationHistoryMessage Message { get; }

        public ConversationHistoryMessageReceivedEventArgs(ConversationHistoryMessage message)
        {
            Message = message;
        }
    }
}