namespace Echo.Core.Connections
{
    public readonly struct DnsResolutionFailedEventArgs
    {
        public XmppConnectionService Connection { get; }
        public string HostOrAddress { get; }
        public string ErrorMessage { get; }
        public int ErrorCode { get; }

        public DnsResolutionFailedEventArgs(XmppConnectionService connection, string hostOrAddress, string errorMessage, int errorCode)
        {
            Connection = connection;
            HostOrAddress = hostOrAddress;
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }
    }
}