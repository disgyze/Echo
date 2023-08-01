using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.Messaging
{
    public abstract class ConversationStore
    {
        public abstract ValueTask<bool> SaveConversationAsync(Conversation conversation, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> UpdateConversationAsync(Conversation conversation, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> DeleteConversationAsync(Conversation conversation, CancellationToken cancellationToken = default);
        public abstract ValueTask<Conversation?> GetConversationAsync(XmppAddress address, CancellationToken cancellationToken = default);
        public abstract IAsyncEnumerable<Conversation> GetConversationsAsync();
    }
}