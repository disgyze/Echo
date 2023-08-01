namespace Echo.Core.Messaging
{
    public readonly struct ConversationMessageMarkedAsReceivedEventArgs
    {
        public XmppAddress AccountAddress { get; }
        public string MessageId { get; }

        public ConversationMessageMarkedAsReceivedEventArgs(XmppAddress accountAddress, string messageId)
        {
            AccountAddress = accountAddress;
            MessageId = messageId;
        }
    }
}