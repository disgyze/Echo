using System.Net;

namespace Echo.Networking.Connections
{
    public readonly struct ProxyOptions
    {
        public EndPoint RemoteEndpoint { get; }
        public string? UserName { get; }
        public string? Password { get; }

        public ProxyOptions(EndPoint remoteEndpoint, string? userName = null, string? password = null)
        {
            RemoteEndpoint = remoteEndpoint;
            UserName = userName;
            Password = password;
        }
    }
}