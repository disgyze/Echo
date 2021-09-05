using System;
using System.Collections.Generic;
using System.Net;

namespace Echo.Core.Connections
{
    public sealed class DnsResolutionSucceedEventArgs : EventArgs
    {
        public IXmppConnection Connection { get; }
        public string Host { get; }
        public IEnumerable<IPAddress> Addresses { get; }

        public DnsResolutionSucceedEventArgs(IXmppConnection connection, string host, IEnumerable<IPAddress> addresses)
        {
            Connection = connection;
            Host = host;
            Addresses = addresses;
        }
    }
}