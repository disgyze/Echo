using System;

namespace Echo.Core.Messaging
{
    public interface IConversationManager
    {
        int Count { get; }
        IConversation ActiveConversation { get; }

        IConversation GetConversation(int conversationIndex);
        IConversation GetConversation(Guid conversationId);
    }
}