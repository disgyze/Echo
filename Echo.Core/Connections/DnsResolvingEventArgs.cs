using System;

namespace Echo.Core.Connections
{
    public sealed class DnsResolvingEventArgs : EventArgs
    {
        public IXmppConnection Client { get; }
        public string Host { get; }

        public DnsResolvingEventArgs(IXmppConnection client, string host)
        {
            Client = client;
            Host = host;
        }
    }
}