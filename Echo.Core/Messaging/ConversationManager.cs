using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.Messaging
{
    public abstract class ConversationManager
    {
        public Conversation? ActiveConversation { get; }

        public abstract bool SetActiveConversation(Conversation conversation);
        public abstract bool ShowConversationUI(Conversation conversation);
        public abstract ValueTask<bool> SendConversationStateAsync(Conversation conversation, ConversationState state, CancellationToken cancellationToken = default);
    }
}