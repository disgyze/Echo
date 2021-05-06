using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public sealed class SocketConnectionFactory : ConnectionFactory
    {
        public static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(30);

        SocketType socketType;
        ProtocolType protocolType;
        AddressFamily addressFamily;

        public SocketConnectionFactory(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
        {
            this.addressFamily = addressFamily;
            this.socketType = socketType;
            this.protocolType = protocolType;
        }

        public override async ValueTask<SocketConnection> OpenAsync(EndPoint endPoint, CancellationToken cancellationToken = default)
        {
            if (endPoint == null) throw new ArgumentNullException(nameof(endPoint));
            cancellationToken.ThrowIfCancellationRequested();

            Socket socket = new Socket(addressFamily, socketType, protocolType);
            socket.NoDelay = protocolType == ProtocolType.Tcp;
            socket.DualMode = addressFamily == AddressFamily.InterNetworkV6;

            try
            {
                await socket.ConnectTaskAsync(endPoint).ConfigureAwait(false);
                return new SocketConnection(socket);
            }
            catch
            {
                socket.Dispose();
                throw;
            }
        }
    }
}