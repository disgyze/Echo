using System;
using System.Net;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public interface IClientSocket : IDisposable, IAsyncDisposable
    {
        event EventHandler Connecting;
        event EventHandler Connected;
        event EventHandler<ClientSocketErrorEventArgs> ConnectionFailed;
        event EventHandler<ClientSocketErrorEventArgs> Error;
        event EventHandler<DnsResolvingEventArgs> DnsResolving;
        event EventHandler<DnsResolvedEventArgs> DnsResolved;
        event EventHandler<DnsResolutionFailedEventArgs> DnsResolutionFailed;
        event EventHandler<CertificateValidatingEventArgs> CertificateValidating;
        event EventHandler<CertificateValidatedEventArgs> CertificateValidated;
        event EventHandler<CertificateValidationFailedEventArgs> CertificateValidationFailed;

        TimeSpan Timeout { get; set; }
        EndPoint? LocalEndpoint { get; }
        EndPoint? RemoteEndpoint { get; }
        int SendBufferSize { get; }
        int ReceiveBufferSize { get; }
        bool IsConnected { get; }
        bool IsConnecting { get; }
        bool IsEncrypted { get; }
        bool IsEncrypting { get; }

        ValueTask<bool> SendAsync(ReadOnlyMemory<byte> data, CancellationToken cancellationToken = default);
        ValueTask<bool> ConnectAsync(EndPoint endpoint, Action<ReadOnlyMemory<byte>> onData, CancellationToken cancellationToken = default);
        ValueTask<bool> DisconnectAsync(CancellationToken cancellationToken = default);
        ValueTask<bool> UpgradeToSslAsync(SslProtocols sslProtocols, CancellationToken cancellationToken = default);
    }
}