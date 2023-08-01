using System;

namespace Echo.Core.Messaging
{
    public readonly struct ConversationHistoryMessageStoreQueryByDate
    {
        public DateTime? BeforeDate { get; }
        public DateTime? AfterDate { get; }

        ConversationHistoryMessageStoreQueryByDate(DateTime? beforeDate = null, DateTime? afterDate = null)
        {
            BeforeDate = beforeDate;
            AfterDate = afterDate;
        }

        public static ConversationHistoryMessageStoreQueryByDate Before(DateTime date)
        {
            return new ConversationHistoryMessageStoreQueryByDate(beforeDate: date);
        }

        public static ConversationHistoryMessageStoreQueryByDate After(DateTime date)
        {
            return new ConversationHistoryMessageStoreQueryByDate(afterDate: date);
        }

        public static ConversationHistoryMessageStoreQueryByDate Between(DateTime beforeDate, DateTime afterDate)
        {
            if (beforeDate > afterDate)
            {
                throw new ArgumentOutOfRangeException();
            }
            return new ConversationHistoryMessageStoreQueryByDate(beforeDate, afterDate);
        }
    }
}