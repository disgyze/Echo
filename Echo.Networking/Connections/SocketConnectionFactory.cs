using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking.Connections
{
    public class SocketConnectionFactory : ConnectionFactory<SocketConnection>
    {
        SocketType socketType;
        ProtocolType protocolType;
        AddressFamily addressFamily;

        public SocketConnectionFactory(AddressFamily addressFamily, SocketType socketType, ProtocolType protocolType)
        {
            this.addressFamily = addressFamily;
            this.socketType = socketType;
            this.protocolType = protocolType;
        }

        public override async ValueTask<SocketConnection> ConnectAsync(EndPoint endpoint, CancellationToken cancellationToken = default)
        {
            if (endpoint is null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            cancellationToken.ThrowIfCancellationRequested();

            Socket socket = CreateSocket();
            try
            {
                await socket.ConnectAsync(endpoint, cancellationToken).ConfigureAwait(false);
                return new SocketConnection(socket);
            }
            catch
            {
                socket.Dispose();
                throw;
            }
        }

        protected virtual Socket CreateSocket()
        {
            Socket socket = new Socket(addressFamily, socketType, protocolType);

            if (protocolType == ProtocolType.Tcp)
            {
                socket.NoDelay = true;
            }

            if (addressFamily == AddressFamily.InterNetworkV6)
            {
                socket.DualMode = true;
            }

            return socket;
        }
    }
}