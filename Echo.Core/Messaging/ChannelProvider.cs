namespace Echo.Core.Messaging
{
    public sealed record ChannelProvider(XmppAddress Address, string Name, ChannelProviderKind Kind);
}