namespace Echo.Core.Connections
{
    public readonly struct DnsResolvingEventArgs
    {
        public XmppConnectionService Connection { get; }
        public string HostOrAddress { get; }

        public DnsResolvingEventArgs(XmppConnectionService connection, string hostOrAddress)
        {
            Connection = connection;
            HostOrAddress = hostOrAddress;
        }
    }
}