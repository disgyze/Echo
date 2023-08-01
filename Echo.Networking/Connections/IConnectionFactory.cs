using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking.Connections
{
    public interface IConnectionFactory
    {
        ValueTask<IConnection> ConnectAsync(EndPoint endpoint, CancellationToken cancellationToken = default);
    }

    public interface IConnectionFactory<TConnection> where TConnection : IConnection
    {
        ValueTask<TConnection> ConnectAsync(EndPoint endpoint, CancellationToken cancellationToken = default);
    }
}