namespace Echo.Core.Messaging
{
    public readonly struct ConversationMessageMarkedAsReadEventArgs
    {
        public XmppAddress AccountAddress { get; }
        public string MessageId { get; }

        public ConversationMessageMarkedAsReadEventArgs(XmppAddress accountAddress, string messageId)
        {
            AccountAddress = accountAddress;
            MessageId = messageId;
        }
    }
}