using System;
using System.Collections.Concurrent;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Networking
{
    public sealed class ClientSocket
    {
        public const int DefaultTimeoutInSeconds = 60;
        public const int DefaultBufferSize = 8192;

        Stream stream = null!;
        Socket socket = null!;
        BlockingCollection<byte[]> outgoingQueue = new BlockingCollection<byte[]>();
        CancellationTokenSource queueCancellationTokenSource = null!;

        public int ReceiveBufferSize { get; set; } = DefaultBufferSize;
        public int SendBufferSize { get; set; } = DefaultBufferSize;
        public TimeSpan Timeout { get; set; } = TimeSpan.FromSeconds(DefaultTimeoutInSeconds);
        public EndPoint? LocalEndpoint { get; private set; }
        public EndPoint? RemoteEndpoint { get; private set; }
        public bool IsEncrypted { get; private set; }
        public bool IsEncrypting { get; private set; }
        public ClientSocketState State { get; private set; }

        public event EventHandler<ClientSocketErrorEventArgs>? ConnectionFailed;
        public event EventHandler<ClientSocketStateChangedEventArgs>? ConnectionStateChanged;
        public event EventHandler<ClientSocketErrorEventArgs>? Error;
        public event EventHandler<DataSentEventArgs>? DataSent;
        public event EventHandler<DataReceivedEventArgs>? DataReceived;
        public event EventHandler<DnsResolvingEventArgs>? DnsResolving;
        public event EventHandler<DnsResolvedEventArgs>? DnsResolved;
        public event EventHandler<DnsResolutionFailedEventArgs>? DnsResolutionFailed;
        public event EventHandler<CertificateValidatingEventArgs>? CertificateValidating;
        public event EventHandler<CertificateValidatedEventArgs>? CertificateValidated;
        public event EventHandler<CertificateValidationFailedEventArgs>? CertificateValidationFailed;

        private void OnConnectionStateChanged(ClientSocketStateChangedEventArgs e)
        {
            State = e.State;
            ConnectionStateChanged?.Invoke(this, e);
        }

        private void OnConnectionFailed(ClientSocketErrorEventArgs e)
        {
            ConnectionFailed?.Invoke(this, e);
        }

        private void OnError(ClientSocketErrorEventArgs e)
        {
            Error?.Invoke(this, e);
        }

        private void OnDataReceived(DataReceivedEventArgs e)
        {
            DataReceived?.Invoke(this, e);
        }

        private void OnDataSent(DataSentEventArgs e)
        {
            DataSent?.Invoke(this, e);
        }

        private void OnCertificateValidating(CertificateValidatingEventArgs e)
        {
            CertificateValidating?.Invoke(this, e);
        }

        private void OnCertificateValidated(CertificateValidatedEventArgs e)
        {
            CertificateValidated?.Invoke(this, e);
        }

        private void OnCertificateValidationFailed(CertificateValidationFailedEventArgs e)
        {
            CertificateValidationFailed?.Invoke(this, e);
        }

        private void OnDnsResolving(DnsResolvingEventArgs e)
        {
            DnsResolving?.Invoke(this, e);
        }

        private void OnDnsResolved(DnsResolvedEventArgs e)
        {
            DnsResolved?.Invoke(this, e);
        }

        private void OnDnsResolutionFailed(DnsResolutionFailedEventArgs e)
        {
            DnsResolutionFailed?.Invoke(this, e);
        }

        public Task<bool> SendAsync(byte[] data, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<bool>(cancellationToken);
            }

            if (socket != null && !socket.Connected)
            {
                return Task.FromResult(false);
            }
            
            outgoingQueue.Add(data);

            return Task.FromResult(true);
        }

        public async Task<bool> ConnectAsync(EndPoint endpoint, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return  Task.FromCanceled<bool>(cancellationToken).GetAwaiter().GetResult();
            }

            if (socket != null && socket.Connected)
            {
                return false;
            }

            try
            {             
                EndPoint dnsEndpoint = await ResolveAsync(endpoint, cancellationToken).ConfigureAwait(false);

                if (dnsEndpoint == null)
                {
                    return false;
                }

                socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                socket.NoDelay = true;
                socket.ReceiveTimeout = Timeout.Milliseconds;
                socket.SendTimeout = Timeout.Milliseconds;
                socket.SendBufferSize = SendBufferSize;
                socket.ReceiveBufferSize = ReceiveBufferSize;

                LocalEndpoint = socket.LocalEndPoint;
                RemoteEndpoint = dnsEndpoint;

                OnConnectionStateChanged(new ClientSocketStateChangedEventArgs(ClientSocketState.Connecting));
                await socket.ConnectTaskAsync(dnsEndpoint).ConfigureAwait(false);
                OnConnectionStateChanged(new ClientSocketStateChangedEventArgs(ClientSocketState.Connected));

                stream = new NetworkStream(socket, false);
                StartDataExchange();
                
                return true;
            }
            catch (SocketException e)
            {
                Reset();
                OnConnectionFailed(new ClientSocketErrorEventArgs(e));
            }

            return false;
        }

        public async Task<bool> DisconnectAsync(CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<bool>(cancellationToken).GetAwaiter().GetResult();
            }

            if (socket != null && socket.Connected)
            {
                await socket.DisconnectTaskAsync().ConfigureAwait(false);

                Reset();
                OnConnectionStateChanged(new ClientSocketStateChangedEventArgs(ClientSocketState.Disconnected));

                return true;
            }

            return false;
        }

        public async Task<bool> UpgradeToSslAsync(SslProtocols protocols, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<bool>(cancellationToken).GetAwaiter().GetResult();
            }

            if (socket != null && !socket.Connected)
            {
                return false;
            }

            StopDataExchange();
            IsEncrypting = true;
            try
            {
                X509Certificate2 remoteCertificate = null!;

                bool CertificateValidationCallback(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors policyErrors)
                {
                    X509Certificate2 temp = new X509Certificate2(certificate);
                    CertificateValidatingEventArgs e = new CertificateValidatingEventArgs(temp, policyErrors == SslPolicyErrors.None);
                    OnCertificateValidating(e);
                    remoteCertificate = temp;
                    return e.Accept;
                }

                SslClientAuthenticationOptions options = new SslClientAuthenticationOptions();
                options.EnabledSslProtocols = protocols;                
                options.CertificateRevocationCheckMode = X509RevocationMode.Online;
                options.TargetHost = RemoteEndpoint switch 
                { 
                    IPEndPoint ip => ip.Address.ToString(), 
                    DnsEndPoint dns => dns.Host, 
                    null => throw new InvalidOperationException($"{nameof(UpgradeToSslAsync)}: RemoteEndpoint cannot be null"),
                    _ => throw new InvalidOperationException($"{nameof(UpgradeToSslAsync)}: RemoteEndpoint has unknown type")
                };

                SslStream sslStream = new SslStream(stream, false, CertificateValidationCallback!);
                await sslStream.AuthenticateAsClientAsync(options, cancellationToken).ConfigureAwait(false);

                stream = sslStream;
                IsEncrypted = true;
                OnCertificateValidated(new CertificateValidatedEventArgs(remoteCertificate));
                StartDataExchange();

                return true;
            }
            catch (AuthenticationException e)
            {
                IsEncrypted = false;
                OnCertificateValidationFailed(new CertificateValidationFailedEventArgs(e));
            }
            catch (SocketException e)
            {
                Reset();
                OnError(new ClientSocketErrorEventArgs(e));
            }
            // TODO ошибка возникает, если указаный протокол не поддерживается сервером
            catch (Win32Exception e)
            {
                Reset();
                OnError(new ClientSocketErrorEventArgs(new SocketException(e.ErrorCode)));
            }
            catch (IOException)
            {
            }
            catch (ObjectDisposedException)
            {
            }
            finally
            {
                IsEncrypting = false;
            }

            return false;
        }

        private async Task<EndPoint> ResolveAsync(EndPoint endpoint, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return Task.FromCanceled<EndPoint>(cancellationToken).GetAwaiter().GetResult();
            }

            //if (endpoint is DnsEndPoint dnsEndpoint)
            //{
            //    return Task.FromResult(dnsEndpoint).GetAwaiter().GetResult();
            //}

            //OnDnsResolving(new DnsResolvingEventArgs(endpoint.ToString()!));
            //try
            //{
            //    IPHostEntry entry = await Dns.GetHostEntryAsync(((DnsEndPoint)endpoint).Host).ConfigureAwait(false);
            //    OnDnsResolved(new DnsResolvedEventArgs(entry.AddressList));
            //    return new DnsEndPoint(entry.AddressList.FirstOrDefault()!.ToString(), ((DnsEndPoint)endpoint).Port);
            //}
            //catch (SocketException e)
            //{
            //    Reset();
            //    OnDnsResolutionFailed(new DnsResolutionFailedEventArgs(e));
            //}

            //return null!;

            // TODO
            switch (endpoint)
            {
                case DnsEndPoint dnsEndpoint:
                {
                    return Task.FromResult(endpoint).GetAwaiter().GetResult();
                }

                case IPEndPoint ipEndpoint:
                {
                    OnDnsResolving(new DnsResolvingEventArgs(ipEndpoint.Address.ToString()));
                    try
                    {

                    }
                    catch (SocketException e)
                    {
                        Reset();
                        OnDnsResolutionFailed(new DnsResolutionFailedEventArgs(e));
                    }
                    return ipEndpoint;
                }
            }

            return null!;
        }

        private void StartDataExchange()
        {
            queueCancellationTokenSource = new CancellationTokenSource();
            _ = Task.Factory.StartNew(ProcessIncomingData, queueCancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
            _ = Task.Factory.StartNew(ProcessOutgoingData, queueCancellationTokenSource.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private void StopDataExchange()
        {
            queueCancellationTokenSource.Cancel();
        }

        private void ProcessOutgoingData()
        {
            try
            {
                byte[] data = null!;

                while (socket != null && socket.Connected && !queueCancellationTokenSource.IsCancellationRequested)
                {
                    data = outgoingQueue.Take();
                    stream.Write(data);
                    OnDataSent(new DataSentEventArgs(data));
                }
            }
            catch (SocketException e)
            {
                Reset();
                OnError(new ClientSocketErrorEventArgs(e));
            }
            catch (IOException)
            {
            }
        }

        private void ProcessIncomingData()
        {
            try
            {
                int bytesRead = 0;
                Span<byte> buffer = new Span<byte>(new byte[DefaultBufferSize]);

                while (socket != null && socket.Connected && !queueCancellationTokenSource.IsCancellationRequested)
                {
                    bytesRead = stream.Read(buffer);

                    if (bytesRead > 0)
                    {
                        OnDataReceived(new DataReceivedEventArgs(bytesRead == buffer.Length ? buffer.ToArray() : buffer.Slice(0, bytesRead).ToArray()));
                    }
                }
            }
            catch (SocketException e)
            {
                Reset();
                OnError(new ClientSocketErrorEventArgs(e));
            }
            catch (IOException)
            {
            }
        }

        private void Reset()
        {
            queueCancellationTokenSource?.Cancel();
            queueCancellationTokenSource?.Dispose();
            queueCancellationTokenSource = null!;
            //outgoingQueue.Clear();
            socket?.Dispose();
            stream?.Dispose();
            socket = null!;
            stream = null!;

            IsEncrypted = false;
            IsEncrypting = false;
            State = ClientSocketState.Disconnected;
        }
    }
}