using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking.Connections
{
    internal sealed class Socks4ProxyConnection : ProxyConnection
    {
        private enum Socks4ProxyCommand : byte
        {
            Connect = 1,
            Bind = 2
        }

        private enum Socks4ProxyReply : byte
        {
            Granted = 90,
            RejectedOrFailed = 91,
            IdentdUnreachable = 92,
            DifferentUserId = 93
        }

        private readonly struct Socks4ProxyRequest
        {
            public string? UserId { get; }
            public IPEndPoint Endpoint { get; }
            public Socks4ProxyCommand Command { get; }

            public Socks4ProxyRequest(IPEndPoint endpoint, Socks4ProxyCommand command, string? userId = null)
            {
                Endpoint = endpoint;
                Command = command;
                UserId = userId;
            }

            public byte[] ToByteArray()
            {
                byte[] header = new byte[] { 4, (byte)Command };
                byte[] address = Endpoint.Address.GetAddressBytes();
                byte[] port = BitConverter.GetBytes((short)Endpoint.Port);
                byte[] userId = UserId != null ? ByteArrayHelper.Combine(Encoding.ASCII.GetBytes(UserId), ByteArrayHelper.GetNullTerminator()) : ByteArrayHelper.GetNullTerminator();

                return ByteArrayHelper.Combine(header, port, address, userId);
            }
        }

        private readonly struct Socks4ProxyResponse
        {
            public IPEndPoint Endpoint { get; }
            public Socks4ProxyReply Reply { get; }

            Socks4ProxyResponse(IPEndPoint endpoint, Socks4ProxyReply reply)
            {
                Endpoint = endpoint;
                Reply = reply;
            }

            public static Socks4ProxyResponse FromByteArray(byte[] buffer)
            {
                byte[] header = new byte[2];
                byte[] port = new byte[2];
                byte[] address = new byte[4];

                Buffer.BlockCopy(buffer, 0, header, 0, header.Length);
                Buffer.BlockCopy(buffer, header.Length, port, 0, port.Length);
                Buffer.BlockCopy(buffer, header.Length + port.Length, address, 0, address.Length);

                Socks4ProxyReply reply = (Socks4ProxyReply)header[1];
                IPAddress destAddress = new IPAddress(address);
                int destPort = BitConverter.ToInt32(port);

                return new Socks4ProxyResponse(new IPEndPoint(destAddress, destPort), reply);
            }
        }

        ProxyOptions options;

        public Socks4ProxyConnection(ProxyOptions options)
        {
            this.options = options;

            switch (options.RemoteEndpoint)
            {
                case IPEndPoint ipEndPoint:
                {
                    if (ipEndPoint.AddressFamily != AddressFamily.InterNetwork)
                    {
                        ThrowOnlyIPv4Allowed();
                    }
                    break;
                }

                case DnsEndPoint dnsEndPoint:
                {
                    if (IPAddress.TryParse(dnsEndPoint.Host, out IPAddress? address) && address.AddressFamily != AddressFamily.InterNetwork)
                    {
                        ThrowOnlyIPv4Allowed();
                    }
                    else
                    {
                        throw new ArgumentException("Domain names not supported.", nameof(options));
                    }
                    break;
                }

                default: throw new ArgumentException("ProxyOptions.RemoteEndpoint has invalid type.", nameof(options));
            }
        }

        public override async ValueTask EstablishAsync(SocketConnection connection, CancellationToken cancellationToken = default)
        {
            if (connection is null)
            {
                throw new ArgumentNullException(nameof(connection));
            }

            cancellationToken.ThrowIfCancellationRequested();

            var stream = connection.Stream;
            var request = new Socks4ProxyRequest((IPEndPoint)options.RemoteEndpoint, Socks4ProxyCommand.Connect);
            await stream.WriteAsync(request.ToByteArray(), cancellationToken).ConfigureAwait(false);

            var responseBytes = new byte[8];
            await stream.ReadAtLeastAsync(responseBytes, responseBytes.Length, true, cancellationToken).ConfigureAwait(false);

            var response = Socks4ProxyResponse.FromByteArray(responseBytes);

            if (response.Reply != Socks4ProxyReply.Granted)
            {
                throw new ProxyException();
            }
        }

        private void ThrowOnlyIPv4Allowed()
        {
            throw new ArgumentException("Only IPv4 address is allowed.", "options");
        }
    }
}