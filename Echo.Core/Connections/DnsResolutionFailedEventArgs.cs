using System;

namespace Echo.Core.Connections
{
    public sealed class DnsResolutionFailedEventArgs : EventArgs
    {
        public IXmppConnection Client { get; }
        public string Host { get; }
        public string Error { get; }
        public int ErrorCode { get; }

        public DnsResolutionFailedEventArgs(IXmppConnection client, string host, string error, int errorCode)
        {
            Client = client;
            Host = host;
            Error = error;
            ErrorCode = errorCode;
        }
    }
}