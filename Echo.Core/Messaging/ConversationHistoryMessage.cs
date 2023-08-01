using System;

namespace Echo.Core.Messaging
{
    public readonly record struct ConversationHistoryMessage(XmppAddress Sender, XmppAddress Target, DateTimeOffset Timestamp, string? Text);
}