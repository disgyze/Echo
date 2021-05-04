using System;
using System.Net;

namespace Echo.Networking
{
    public sealed partial class ProxyConnectionFactory
    {
        private enum Socks4ProxyCommand : byte
        {
            Connect = 1,
            Bind = 2
        }

        private enum Socks4ProxyReply : byte
        {
            Granted = 0x5A,
            RejectedOrFailed = 0x5B
        }

        private struct Socks4ProxyRequest
        {
            public IPEndPoint EndPoint { get; }
            public Socks4ProxyCommand Command { get; }

            public Socks4ProxyRequest(IPEndPoint endPoint, Socks4ProxyCommand command)
            {
                EndPoint = endPoint;
                Command = command;
            }

            public byte[] ToByteArray()
            {
                byte[] header = new byte[] { 4, (byte)Command };
                byte[] address = EndPoint.Address.GetAddressBytes();
                byte[] port = BitConverter.GetBytes((short)EndPoint.Port);
                byte[] request = new byte[header.Length + address.Length + port.Length];

                Buffer.BlockCopy(header, 0, request, 0, header.Length);
                Buffer.BlockCopy(port, 0, request, header.Length, port.Length);
                Buffer.BlockCopy(address, 0, request, header.Length + port.Length, address.Length);

                return request;
            }
        }

        private struct Socks4ProxyResponse
        {
            public IPEndPoint EndPoint { get; }
            public Socks4ProxyReply Reply { get; }

            Socks4ProxyResponse(IPEndPoint endPoint, Socks4ProxyReply reply)
            {
                EndPoint = endPoint;
                Reply = reply;
            }

            public static Socks4ProxyResponse FromByteArray(byte[] array)
            {
                byte[] header = new byte[2];
                byte[] port = new byte[2];
                byte[] address = new byte[4];

                Buffer.BlockCopy(array, 0, header, 0, header.Length);
                Buffer.BlockCopy(array, header.Length, port, 0, port.Length);
                Buffer.BlockCopy(array, header.Length + port.Length, address, 0, address.Length);

                Socks4ProxyReply reply = (Socks4ProxyReply)header[1];
                IPAddress destAddress = new IPAddress(address);
                int destPort = BitConverter.ToInt32(port);

                return new Socks4ProxyResponse(new IPEndPoint(destAddress, destPort), reply);
            }
        }
    }
}