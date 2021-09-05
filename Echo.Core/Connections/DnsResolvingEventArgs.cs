using System;

namespace Echo.Core.Connections
{
    public sealed class DnsResolvingEventArgs : EventArgs
    {
        public IXmppConnection Connection { get; }
        public string Host { get; }

        public DnsResolvingEventArgs(IXmppConnection connection, string host)
        {
            Connection = connection;
            Host = host;
        }
    }
}