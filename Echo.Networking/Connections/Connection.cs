using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking.Connections
{
    public abstract class Connection : IConnection
    {
        int disposed = 0;

        public abstract EndPoint? LocalEndpoint { get; }
        public abstract EndPoint? RemoteEndpoint { get; }

        public void Dispose()
        {
            var task = CloseAsync(ConnectionCloseMethod.GracefulShutdown);

            if (task.IsCompleted)
            {
                task.GetAwaiter().GetResult();
            }
            else
            {
                task.AsTask().GetAwaiter().GetResult();
            }
        }

        public ValueTask DisposeAsync()
        {
            return CloseAsync(ConnectionCloseMethod.GracefulShutdown);
        }        

        public async ValueTask CloseAsync(ConnectionCloseMethod method, CancellationToken cancellationToken = default)
        {
            if (Interlocked.Exchange(ref disposed, 1) != 0)
            {
                return;
            }

            await CloseAsyncCore(method, cancellationToken).ConfigureAwait(false);
            GC.SuppressFinalize(this);
        }

        protected abstract ValueTask CloseAsyncCore(ConnectionCloseMethod method, CancellationToken cancellationToken = default);
    }
}