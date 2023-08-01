using System;

namespace Echo.Core.Messaging
{
    public readonly struct ConversationHistoryMessageStoreQueryById
    {
        public string? BeforeMessageId { get; }
        public string? AfterMessageId { get; }

        ConversationHistoryMessageStoreQueryById(string? beforeMessageId = null, string? afterMessageId = null)
        {
            BeforeMessageId = beforeMessageId;
            AfterMessageId = afterMessageId;
        }

        public static ConversationHistoryMessageStoreQueryById Before(string messageId)
        {
            if (messageId is null)
            {
                throw new ArgumentNullException(nameof(messageId));
            }

            if (string.IsNullOrWhiteSpace(messageId))
            {
                throw new ArgumentException(null, nameof(messageId));
            }

            return new ConversationHistoryMessageStoreQueryById(beforeMessageId: messageId);
        }

        public static ConversationHistoryMessageStoreQueryById After(string messageId)
        {
            if (messageId is null)
            {
                throw new ArgumentNullException(nameof(messageId));
            }

            if (string.IsNullOrWhiteSpace(messageId))
            {
                throw new ArgumentException(null, nameof(messageId));
            }

            return new ConversationHistoryMessageStoreQueryById(afterMessageId: messageId);
        }

        public static ConversationHistoryMessageStoreQueryById Between(string beforeMessageId, string afterMessageId)
        {
            if (beforeMessageId is null)
            {
                throw new ArgumentNullException(nameof(beforeMessageId));
            }

            if (afterMessageId is null)
            {
                throw new ArgumentNullException(nameof(afterMessageId));
            }

            if (string.IsNullOrWhiteSpace(beforeMessageId))
            {
                throw new ArgumentException(null, nameof(beforeMessageId));
            }

            if (string.IsNullOrWhiteSpace(afterMessageId))
            {
                throw new ArgumentException(null, nameof(afterMessageId));
            }

            return new ConversationHistoryMessageStoreQueryById(beforeMessageId, afterMessageId);
        }
    }
}