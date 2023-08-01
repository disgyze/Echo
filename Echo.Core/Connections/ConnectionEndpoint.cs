using System.Net;

namespace Echo.Core.Connections
{
    public sealed class ConnectionEndpoint
    {
        public IPAddress Address { get; }
        public string Host { get; }
        public int Port { get; }

        public ConnectionEndpoint(IPAddress address, string host, int port)
        {
            Address = address;
            Host = host;
            Port = port;
        }
    }
}