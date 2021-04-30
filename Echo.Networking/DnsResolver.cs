using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public sealed class DnsResolver : IDisposable, IAsyncDisposable
    {
        public async ValueTask<IPAddress[]> ResolveAsync(string hostName, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return ValueTask.FromCanceled<IPAddress[]>(cancellationToken).GetAwaiter().GetResult();
            }
            return await System.Net.Dns.GetHostAddressesAsync(hostName);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }
    }
}