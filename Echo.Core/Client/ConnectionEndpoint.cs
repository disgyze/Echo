using System;
using System.Net;

namespace Echo.Core.Client
{
    public sealed class ConnectionEndpoint
    {
        public IPAddress? Address { get; }
        public string? Host { get; }
        public int Port { get; }

        public ConnectionEndpoint(IPAddress address, int port) : this(address, null, port)
        {
        }

        public ConnectionEndpoint(string host, int port) : this(null, host, port)
        {
        }

        public ConnectionEndpoint(IPAddress? address, string? host, int port)
        {
            if (address == null && host == null)
            {
                throw new ArgumentNullException();
            }

            if (port < IPEndPoint.MinPort || port > IPEndPoint.MaxPort)
            {
                throw new ArgumentOutOfRangeException(nameof(port));
            }

            Address = address;
            Host = host;
            Port = port;
        }

        public override string ToString()
        {
            return $"{(Host != null ? Host : Address)}:{Port}";
        }
    }
}