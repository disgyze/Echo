using System.Collections.Immutable;

namespace Echo.Core.Messaging
{
    public readonly record struct ConversationMessage(XmppAddress Sender, XmppAddress Target, string? Text, ImmutableArray<object> Metadata);
}