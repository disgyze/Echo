namespace Echo.Core.Messaging
{
    public readonly struct ChannelServiceInformation
    {
        public XmppAddress Address { get; }
        public ChannelServiceKind Kind { get; }

        public ChannelServiceInformation(XmppAddress address, ChannelServiceKind kind)
        {
            Address = address;
            Kind = kind;
        }
    }
}