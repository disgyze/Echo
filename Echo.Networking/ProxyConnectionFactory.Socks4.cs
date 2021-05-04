using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Buffers;

namespace Echo.Networking
{
    public sealed partial class ProxyConnectionFactory
    {
        private enum Socks4ProxyCommand : byte
        {
            StreamConnection = 1,
            PortBinding = 2
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
                Buffer.BlockCopy(address, 0, request, header.Length, address.Length);
                Buffer.BlockCopy(port, 0, request, address.Length, port.Length);

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
                byte version = array[0];
                byte reply = array[1];
                byte[] port = new byte[2];
                byte[] address = new byte[4];

                Buffer.BlockCopy(array, 2, port, 0, port.Length);
                Buffer.BlockCopy(array, 2 + port.Length, address, 0, address.Length);

                return new Socks4ProxyResponse(
                    new IPEndPoint(new IPAddress(address), BitConverter.ToInt32(port)), 
                    (Socks4ProxyReply)reply);
            }
        }
    }
}