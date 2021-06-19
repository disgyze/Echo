using System;
using System.Collections.Generic;
using System.Net;

namespace Echo.Core.Connections
{
    public sealed class DnsResolutionSucceedEventArgs : EventArgs
    {
        public IXmppConnection Client { get; }
        public string Host { get; }
        public IEnumerable<IPAddress> Addresses { get; }

        public DnsResolutionSucceedEventArgs(IXmppConnection client, string host, IEnumerable<IPAddress> addresses)
        {
            Client = client;
            Host = host;
            Addresses = addresses;
        }
    }
}