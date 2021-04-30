using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Echo.Networking
{
    public sealed class ProxyInformation
    {
        public EndPoint EndPoint { get; }
        public ProxyCredential Credential { get; }
    }
}