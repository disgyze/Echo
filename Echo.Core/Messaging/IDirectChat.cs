namespace Echo.Core.Messaging
{
    public interface IDirectChat : IConversation
    {
        XmppUri Address { get; }
        ChatState State { get; }
    }
}