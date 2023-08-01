using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.Messaging
{
    public abstract class ConversationMessageStore
    {
        public abstract ValueTask SendAsync(XmppAddress address, ConversationMessage message, CancellationToken cancellationToken = default);
        public abstract ValueTask MarkAsReadAsync(string messageId, CancellationToken cancellationToken = default);
        public abstract ValueTask MarkAsReceivedAsync(string messageId, CancellationToken cancellationToken = default);
    }
}