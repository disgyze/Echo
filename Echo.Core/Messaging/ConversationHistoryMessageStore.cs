using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.Messaging
{
    public abstract class ConversationHistoryMessageStore
    {
        public const int DefaultLimit = 50;

        public abstract ValueTask<ConversationHistoryMessage?> GetMessageAsync(XmppAddress conversationAddress, string messageId, CancellationToken cancellationToken = default);
        public abstract IAsyncEnumerable<ConversationHistoryMessage> GetMessagesAsync(int offset, int limit = DefaultLimit);
        public abstract IAsyncEnumerable<ConversationHistoryMessage> GetMessagesAsync(XmppAddress conversationAddress, ConversationHistoryMessageStoreQueryById query, int offset, int limit = DefaultLimit, CancellationToken cancellationToken = default);
        public abstract IAsyncEnumerable<ConversationHistoryMessage> GetMessagesAsync(XmppAddress conversationAddress, ConversationHistoryMessageStoreQueryByDate query, int offset, int limit = DefaultLimit, CancellationToken cancellationToken = default);
    }
}