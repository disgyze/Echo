using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Echo.Networking.Connections;
using Echo.Xmpp.ElementModel;
using Echo.Xmpp.Parser;

namespace Echo.Xmpp.Connections
{
    public sealed class XmppSocketConnection : XmppConnection
    {
        readonly struct ConnectionEndpoint
        {
            public string Host { get; }
            public IPAddress Address { get; }
            public int Port { get; }

            public ConnectionEndpoint(string host, IPAddress address, int port)
            {
                Host = host;
                Address = address;
                Port = port;
            }
        }

        static readonly SocketConnectionFactory IPv4ConnectionFactory = new SocketConnectionFactory(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static readonly SocketConnectionFactory IPv6ConnectionFactory = new SocketConnectionFactory(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
        static readonly XmlParserContext ParserContext;
        static readonly XmlReaderSettings ReaderSettings;
        static readonly XmlWriterSettings WriterSettings;

        int disposed = 0;
        ConnectionEndpoint connectionEndpoint;
        SocketConnection? connection;
        Task? readingTask;
        CancellationTokenSource? readingCancellation;
        XmlReader? reader;
        XmlWriter? writer;
        bool isConnecting = false;
        bool isConnected = false;
        bool isEncrypted = false;
        string? serverDomainName = null;

        public override EndPoint? LocalEndpoint => connection?.LocalEndpoint;
        public override EndPoint? RemoteEndpoint => connection?.RemoteEndpoint;
        public override bool IsConnecting => isConnecting;
        public override bool IsConnected => isConnected;
        public override bool IsEncrypted => isEncrypted;

        static XmppSocketConnection()
        {
            var namespaceManager = new XmlNamespaceManager(new NameTable());
            namespaceManager.AddNamespace(string.Empty, "jabber:client");
            namespaceManager.AddNamespace("stream", "http://etherx.jabber.org/streams");

            ParserContext = new XmlParserContext(null, namespaceManager, null, XmlSpace.None, Encoding.UTF8);

            ReaderSettings = new XmlReaderSettings()
            {
                Async = true,
                CheckCharacters = false,
                ConformanceLevel = ConformanceLevel.Fragment,
                ValidationType = ValidationType.None,
                DtdProcessing = DtdProcessing.Ignore,
                IgnoreComments = true,
                IgnoreWhitespace = true,
                IgnoreProcessingInstructions = true,
                CloseInput = false
            };

            WriterSettings = new XmlWriterSettings()
            {
                Async = true,
                CheckCharacters = false,
                CloseOutput = false,
                Encoding = Encoding.UTF8,
                ConformanceLevel = ConformanceLevel.Fragment,
                NamespaceHandling = NamespaceHandling.OmitDuplicates
            };
        }

        public XmppSocketConnection(IXmppElementFactory elementFactory) : base(elementFactory)
        {
        }

        public override void Dispose()
        {
            var task = DisposeAsync();

            if (task.IsCompleted)
            {
                task.GetAwaiter().GetResult();
            }
            else
            {
                task.AsTask().GetAwaiter().GetResult();
            }
        }

        public override async ValueTask DisposeAsync()
        {
            if (Interlocked.Exchange(ref disposed, 1) != 0)
            {
                return;
            }
            await CloseAsync(closeStream: false);
        }

        public override async ValueTask<bool> OpenAsync(EndPoint endpoint, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (endpoint is null)
            {
                throw new ArgumentNullException(nameof(endpoint));
            }

            isConnecting = true;
            OnConnecting();
            try
            {
                connectionEndpoint = await ResolveAsync(endpoint, cancellationToken).ConfigureAwait(false);

                var connectionFactory = connectionEndpoint.Address.AddressFamily switch
                {
                    AddressFamily.InterNetwork => IPv4ConnectionFactory,
                    AddressFamily.InterNetworkV6 => IPv6ConnectionFactory,
                    _ => throw new NotSupportedException("Unsupported address family")
                };
                connection = await connectionFactory.ConnectAsync(new IPEndPoint(connectionEndpoint.Address, connectionEndpoint.Port), cancellationToken).ConfigureAwait(false);

                RestartStream(connection.Stream);

                isConnected = true;
                OnConnected();

                return true;
            }
            catch (SocketException e)
            {
                Reset();
                OnConnectionFailed(new XmppConnectionErrorEventArgs(e.Message, e.ErrorCode));
            }

            return false;

            static async Task<ConnectionEndpoint> ResolveAsync(EndPoint endpoint, CancellationToken cancellationToken = default)
            {
                string? hostNameOrAddress = null;
                int port = 5222;

                switch (endpoint)
                {
                    case IPEndPoint ipEndpoint:
                    {
                        hostNameOrAddress = ipEndpoint.Address.ToString();
                        port = ipEndpoint.Port;
                        break;
                    }

                    case DnsEndPoint dnsEndpoint:
                    {
                        hostNameOrAddress = dnsEndpoint.Host;
                        port = dnsEndpoint.Port;
                        break;
                    }

                    default: throw new NotSupportedException();
                }

                var hostEntry = await Dns.GetHostEntryAsync(hostNameOrAddress, cancellationToken).ConfigureAwait(false);
                return new ConnectionEndpoint(hostEntry.HostName, hostEntry.AddressList.First(), port);
            }
        }

        public override async ValueTask<bool> CloseAsync(bool closeStream = true, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (!isConnected)
            {
                return false;
            }

            try
            {
                if (connection is SocketConnection tempConnection)
                {
                    if (closeStream)
                    {
                        await CloseStreamAsync(cancellationToken).ConfigureAwait(false);
                    }
                    await tempConnection.CloseAsync(ConnectionCloseMethod.Immediate, cancellationToken).ConfigureAwait(false);
                }
            }
            catch
            {
            }
            finally
            {
                Reset();
                OnDisconnected();
            }

            return true;
        }

        public override async ValueTask<bool> OpenStreamAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (!isConnected)
            {
                return false;
            }

            var tempWriter = writer;

            if (tempWriter is null)
            {
                return false;
            }

            try
            {
                tempWriter.WriteStartElement("stream", "stream", XmppCoreNamespace.Stream);
                tempWriter.WriteAttributeString("xmlns", XmppCoreNamespace.Client);
                tempWriter.WriteAttributeString("version", "1.0");
                tempWriter.WriteAttributeString("to", connectionEndpoint.Host);
                tempWriter.WriteRaw("");

                await tempWriter.FlushAsync().ConfigureAwait(false);

                return true;
            }
            catch (InvalidOperationException)
            {
                Reset();
            }
            catch (IOException)
            {
                Reset();
            }

            return false;
        }

        public override async ValueTask<bool> CloseStreamAsync(CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (!isConnected)
            {
                return false;
            }

            if (writer is XmlWriter tempWriter)
            {
                try
                {
                    await tempWriter.WriteEndElementAsync().ConfigureAwait(false);
                    await tempWriter.FlushAsync().ConfigureAwait(false);
                    return true;
                }
                catch (InvalidOperationException)
                {
                    Reset();
                }
                catch (IOException)
                {
                    Reset();
                }
            }

            return false;
        }

        public override async ValueTask<bool> SendAsync(XElement element, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (element is null)
            {
                throw new ArgumentNullException(nameof(element));
            }

            if (!isConnected)
            {
                return false;
            }

            if (writer is XmlWriter tempWriter)
            {
                try
                {
                    await element.WriteToAsync(tempWriter, cancellationToken).ConfigureAwait(false);
                    await tempWriter.FlushAsync().ConfigureAwait(false);
                    OnXmlElementSent(new XmppConnectionXmlElementEventArgs(element));

                    return true;
                }
                catch (InvalidOperationException)
                {
                    Reset();
                }
                catch (IOException)
                {
                    Reset();
                }
            }

            return false;
        }

        public async ValueTask SecureAsync(SslProtocols sslProtocols = SslProtocols.Tls12 | SslProtocols.Tls13, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();
            cancellationToken.ThrowIfCancellationRequested();

            if (!isConnected)
            {
                return;
            }

            var tempConnection = connection;

            if (tempConnection is null)
            {
                return;
            }

            try
            {
                await StopReadingAsync().ConfigureAwait(false);

                var options = new SslClientAuthenticationOptions();
                options.TargetHost = serverDomainName ?? connectionEndpoint.Host;
                options.EnabledSslProtocols = sslProtocols;
                options.CertificateRevocationCheckMode = X509RevocationMode.Online;
                options.RemoteCertificateValidationCallback += (s, certificate, chain, policyErrors) => OnCertificateValidation(new XmppConnectionCertificateValidationEventArgs(certificate, chain, policyErrors));

                connection = await SslSocketConnectionFactory.SecureAsync(tempConnection, options, cancellationToken).ConfigureAwait(false);
                
                isEncrypted = true;
                OnCertificateValidationSucceed();

                RestartStream(connection.Stream);
            }
            catch (AuthenticationException e)
            {
                Reset();
                OnCertificateValidationFailed(new XmppConnectionCertificateValidationFailedEventArgs(e.InnerException is null ? e.Message : e.InnerException.Message));
            }
            catch (IOException e)
            {
                Reset();
                OnCertificateValidationFailed(new XmppConnectionCertificateValidationFailedEventArgs(e.Message));
            }
        }

        private void RestartStream(Stream stream)
        {
            reader = CreateReader(stream);
            writer = CreateWriter(stream);

            readingCancellation = new CancellationTokenSource();
            readingTask = StartReadingAsync();
        }

        private XmlReader CreateReader(Stream stream)
        {
            return XmlReader.Create(stream, ReaderSettings, ParserContext);
        }

        private XmlWriter CreateWriter(Stream stream)
        {
            return XmlWriter.Create(stream, WriterSettings);
        }

        private Task StopReadingAsync()
        {
            var readingCancellation = this.readingCancellation;
            var readingTask = this.readingTask;
            var reader = this.reader;

            if (readingCancellation is null || readingTask is null || reader is null)
            {
                return Task.CompletedTask;
            }

            try
            {
                readingCancellation.Cancel();
                reader.Close();
            }
            catch (ObjectDisposedException)
            {
            }

            return readingTask;
        }

        private Task StartReadingAsync()
        {
            var tempReader = reader;
            var tempReadingCancellation = readingCancellation;

            if (tempReader is null || tempReadingCancellation is null)
            {
                return Task.CompletedTask;
            }

            Action readingLoop = () =>
            {
                var reader = tempReader;

                if (reader is null)
                {
                    return;
                }

                try
                {
                    bool streamClosed = false;

                    while (reader.Read())
                    {
                        switch (reader.NodeType)
                        {
                            case XmlNodeType.Element:
                            {
                                if (reader.Name.Equals("stream:stream", StringComparison.OrdinalIgnoreCase) && reader.NamespaceURI.Equals(XmppCoreNamespace.Stream, StringComparison.OrdinalIgnoreCase))
                                {
                                    for (int i = 0; i < reader.AttributeCount; i++)
                                    {
                                        reader.MoveToAttribute(i);
                                        
                                        if (string.Equals(reader.LocalName, "from", StringComparison.OrdinalIgnoreCase))
                                        {
                                            serverDomainName = reader.Value;
                                            break;
                                        }
                                    }
                                    OnStreamOpened();
                                    continue;
                                }

                                using (var innerReader = reader.ReadSubtree())
                                {
                                    OnXmlElementReceived(new XmppConnectionXmlElementEventArgs(CreateElement(innerReader)));
                                }

                                break;
                            }

                            case XmlNodeType.EndElement:
                            {
                                streamClosed = true;
                                OnStreamClosed();
                                break;
                            }
                        }
                    }

                    if (streamClosed)
                    {
                        Reset();
                        OnDisconnected();
                    }
                }
                catch (XmlException e)
                {
                    OnXmlParsingFailed(new XmppConnectionXmlParsingFailedEventArgs(e));
                }
                catch (IOException)
                {
                    Reset();
                }
            };

            return Task.Factory.StartNew(readingLoop, tempReadingCancellation.Token, TaskCreationOptions.LongRunning, TaskScheduler.Default);
        }

        private void Reset()
        {
            readingCancellation?.Dispose();
            readingCancellation = null;
            readingTask = null;

            reader = null;
            writer = null;

            connection?.Dispose();
            connection = null;

            isConnected = false;
            isConnecting = false;
            isEncrypted = false;
        }

        private void ThrowIfDisposed()
        {
            if (disposed != 0)
            {
                throw new ObjectDisposedException(nameof(XmppSocketConnection));
            }
        }
    }
}