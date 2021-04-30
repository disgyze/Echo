using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public interface IProxyFactory
    {
        Task<Socket> ConnectAsync(EndPoint proxyEndpoint, EndPoint targetEndpoint, string? userName = null, string? password = null, CancellationToken cancellationToken = default);
    }
}