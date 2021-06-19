using System;

namespace Echo.Core.Connections
{
    public sealed class DnsResolvingEventArgs : EventArgs
    {
        public IXmppClient Client { get; }
        public string Host { get; }

        public DnsResolvingEventArgs(IXmppClient client, string host)
        {
            Client = client;
            Host = host;
        }
    }
}