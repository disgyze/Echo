using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Echo.Networking
{
    internal static class SocketExtensions
    {
        //public static Task ConnectTaskAsync(this Socket socket, EndPoint endpoint)
        //{
        //    return Task.Factory.FromAsync(
        //        socket.BeginConnect(endpoint, null, null),
        //        socket.EndConnect);
        //}

        //public static Task ConnectTaskAsync(this Socket socket, string host, int port)
        //{
        //    return Task.Factory.FromAsync(
        //        socket.BeginConnect(host, port, null, null),
        //        socket.EndConnect);
        //}

        //public static Task ConnectTaskAsync(this Socket socket, IPAddress address, int port)
        //{
        //    return Task.Factory.FromAsync(
        //        socket.BeginConnect(address, port, null, null),
        //        socket.EndConnect);
        //}

        //public static Task DisconnectTaskAsync(this Socket socket, bool reuseSocket = false)
        //{
        //    return Task.Factory.FromAsync(
        //        socket.BeginDisconnect(reuseSocket, null, null),
        //        socket.EndDisconnect);
        //}
    }
}