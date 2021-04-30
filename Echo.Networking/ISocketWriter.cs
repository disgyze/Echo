using System;
using System.Collections.Concurrent;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public interface ISocketWriter : IDisposable, IAsyncDisposable
    {
        bool IsRunning { get; }
        Task StartAsync(ConcurrentQueue<byte[]> queue, Action<SocketException>? onError = null);
        Task CancelAsync();
    }
}