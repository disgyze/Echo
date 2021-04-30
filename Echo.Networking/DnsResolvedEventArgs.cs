using System;
using System.Collections.Generic;
using System.Net;

namespace Echo.Networking
{
    public sealed class DnsResolvedEventArgs : EventArgs
    {
        public IEnumerable<IPAddress> Addresses { get; }

        public DnsResolvedEventArgs(IEnumerable<IPAddress> addresses)
        {
            Addresses = addresses;
        }
    }
}