using System.Collections.Generic;
using System.Net;

namespace Echo.Core.Connections
{
    public readonly struct DnsResolutionSucceedEventArgs
    {
        public XmppConnectionService Connection { get; }
        public string Host { get; }
        public IEnumerable<IPAddress> Addresses { get; }

        public DnsResolutionSucceedEventArgs(XmppConnectionService connection, string host, IEnumerable<IPAddress> addresses)
        {
            Connection = connection;
            Host = host;
            Addresses = addresses;
        }
    }
}