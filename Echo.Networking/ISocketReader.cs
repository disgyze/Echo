using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public interface ISocketReader : IDisposable, IAsyncDisposable
    {
        bool IsRunning { get; }
        Task StartAsync(Action<byte[]> onData, Action<SocketException>? onError = null);
        Task CancelAsync();
    }
}