using System.Net;
using System.Net.Sockets;

namespace Echo.Core.Connections
{
    public sealed class ConnectionEndPoint : EndPoint
    {
        public IPAddress Address { get; }
        public string Host { get; }
        public int Port { get; }
        public override AddressFamily AddressFamily => Address.AddressFamily;

        public ConnectionEndPoint(IPAddress address, string host, int port)
        {
            Address = address;
            Host = host;
            Port = port;
        }
    }
}