using System.Net;

namespace Echo.Networking
{
    public sealed class ProxyOptions
    {
        public EndPoint EndPoint { get; }
        public string? UserName { get; }
        public string? Password { get; }

        public ProxyOptions(EndPoint remoteEndPoint, string? userName = null, string? password = null)
        {
            EndPoint = remoteEndPoint;
            UserName = userName;
            Password = password;
        }
    }
}