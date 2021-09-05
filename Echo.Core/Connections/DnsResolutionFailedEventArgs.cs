using System;

namespace Echo.Core.Connections
{
    public sealed class DnsResolutionFailedEventArgs : EventArgs
    {
        public IXmppConnection Connection { get; }
        public string Host { get; }
        public string Error { get; }
        public int ErrorCode { get; }

        public DnsResolutionFailedEventArgs(IXmppConnection connection, string host, string error, int errorCode)
        {
            Connection = connection;
            Host = host;
            Error = error;
            ErrorCode = errorCode;
        }
    }
}