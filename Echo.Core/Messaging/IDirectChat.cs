namespace Echo.Core.Messaging
{
    public interface IDirectChat : IConversation
    {
        XmppAddress Address { get; }
        ChatState State { get; }
    }
}