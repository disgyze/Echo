namespace Echo.Core.Messaging
{
    public readonly struct ChannelServiceInformation
    {
        public XmppUri Address { get; }
        public ChannelServiceKind Kind { get; }

        public ChannelServiceInformation(XmppUri address, ChannelServiceKind kind)
        {
            Address = address;
            Kind = kind;
        }
    }
}