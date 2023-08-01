namespace Echo.Core.Messaging
{
    public readonly struct ConversationMessageReceivedEventArgs
    {
        public ConversationMessage Message { get; }
        
        public ConversationMessageReceivedEventArgs(ConversationMessage message)
        {
            Message = message;
        }
    }
}