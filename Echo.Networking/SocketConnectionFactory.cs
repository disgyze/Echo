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
        TimeSpan timeout;

        public SocketConnectionFactory(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType, TimeSpan timeout)
        {
            this.addressFamily = addressFamily;
            this.socketType = socketType;
            this.protocolType = protocolType;
            this.timeout = timeout;
        }

        public override async ValueTask<SocketConnection> OpenAsync(EndPoint endPoint, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                await ValueTask.FromCanceled(cancellationToken);
            }

            Socket socket = new Socket(addressFamily, socketType, protocolType);
            socket.NoDelay = true;
            socket.DualMode = addressFamily == AddressFamily.InterNetworkV6;
            socket.SendTimeout = (int)timeout.TotalMilliseconds;
            socket.ReceiveTimeout = (int)timeout.TotalMilliseconds;
            await socket.ConnectTaskAsync(endPoint).ConfigureAwait(false);

            return new SocketConnection(socket);
        }
    }
}