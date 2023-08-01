using System;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking.Connections
{
    public interface IConnection : IDisposable, IAsyncDisposable
    {
        ValueTask CloseAsync(ConnectionCloseMethod method, CancellationToken cancellationToken = default);
    }
}