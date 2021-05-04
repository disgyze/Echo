using System.Net;

namespace Echo.Networking
{
    public sealed class ProxyOptions
    {
        public EndPoint RemoteEndPoint { get; }
        public string? UserName { get; }
        public string? Password { get; }

        public ProxyOptions(EndPoint remoteEndPoint, string? userName = null, string? password = null)
        {
            RemoteEndPoint = remoteEndPoint;
            UserName = userName;
            Password = password;
        }
    }
}