using System;
using System.Buffers;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Echo.Core.Extensibility.Eventing;
using Echo.Networking;
using Echo.Networking.Connections;
using Echo.Xmpp.Parser;

namespace Echo.Core.Connections
{
    //public sealed class XmppSocketConnection : XmppConnection
    //{
    //    const string streamOpenXml = "<?xml version='1.0'?><stream:stream to='{0}' version='1.0' xmlns='jabber:client' xmlns:stream='http://etherx.jabber.org/streams'>";
    //    const string streamCloseXml = "</stream:stream>";

    //    bool disposed = false;
    //    XmppParser parser;
    //    EventPublisher eventPublisher;
    //    SocketConnection? connection;
    //    Task? readingTask;
    //    CancellationTokenSource? cancellationTokenSource;
    //    ConnectionSettings connectionSettings;

    //    public XmppSocketConnection(EventPublisher eventPublisher, ConnectionSettings connectionSettings)
    //    {
    //        parser = new XmppParser(XmppElementManager.CreateDefault());
    //        parser.ElementParsed += EventParserElementParsed;
    //        parser.StreamOpened += EventParserStreamOpened;
    //        parser.StreamClosed += EventParserStreamClosed;
    //        parser.ParsingFailed += EventParserParsingFailed;

    //        this.eventPublisher = eventPublisher;
    //        this.connectionSettings = connectionSettings;
    //    }

    //    public override void Dispose()
    //    {
    //        if (disposed)
    //        {
    //            return;
    //        }

    //        Reset();
    //        disposed = true;

    //        GC.SuppressFinalize(this);
    //    }

    //    public async ValueTask<bool> OpenAsync(bool reopenIfClosed = false, bool rejoinChannels = true, CancellationToken cancellationToken = default)
    //    {
    //        ThrowIfDisposed();
    //        cancellationToken.ThrowIfCancellationRequested();

    //        if (State != ConnectionState.Closed && reopenIfClosed)
    //        {
    //            await CloseAsync(cancellationToken).ConfigureAwait(false);
    //        }
    //        else
    //        {
    //            return false;
    //        }

    //        RemoteEndpoint = await ResolveRemoteEndpointAsync().ConfigureAwait(false);

    //        if (RemoteEndpoint is null)
    //        {
    //            return false;
    //        }

    //        SetState(ConnectionState.Opening);
    //        try
    //        {
    //            cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

    //            connection = (SocketConnection)await OpenSocketConnectionAsync(RemoteEndpoint, cancellationToken).ConfigureAwait(false);
    //            LocalEndpoint = await ResolveLocalEndpointAsync().ConfigureAwait(false);

    //            readingTask = StartReadingAsync(cancellationTokenSource.Token);
    //            SetState(ConnectionState.Opened);
    //        }
    //        catch (SocketException e)
    //        {
    //            Reset();
    //            _ = eventPublisher.PublishAsync(new ConnectionFailedEventArgs(this, e.Message, e.ErrorCode));
    //            return false;
    //        }

    //        return true;
    //    }

    //    public async ValueTask<bool> CloseAsync(CancellationToken cancellationToken = default)
    //    {
    //        ThrowIfDisposed();
    //        cancellationToken.ThrowIfCancellationRequested();

    //        if (connection is not null && connection.Socket.Connected)
    //        {
    //            await connection.CloseAsync(ConnectionCloseMethod.Immediate, cancellationToken).ConfigureAwait(false);
    //            await StopReadingAsync().ConfigureAwait(false);
    //            Reset();
    //            return true;
    //        }

    //        return false;
    //    }

    //    public override async ValueTask<bool> SendAsync(XElement element, CancellationToken cancellationToken = default)
    //    {
    //        ThrowIfDisposed();
    //        cancellationToken.ThrowIfCancellationRequested();

    //        if (element is null || State != ConnectionState.Opened)
    //        {
    //            return false;
    //        }

    //        if (await eventPublisher.PublishAsync(new XmppElementSentEventArgs(this, element)).ConfigureAwait(false) != EventResult.Stop)
    //        {
    //            byte[] data = Encoding.UTF8.GetBytes(element.ToString());
    //            return await WriteAsync(data, cancellationToken).ConfigureAwait(false);
    //        }

    //        return true;
    //    }

    //    public ValueTask<bool> WriteAsync(byte[] data, int offset, int count, CancellationToken cancellationToken = default)
    //    {
    //        return WriteAsync(new ReadOnlyMemory<byte>(data, offset, count), cancellationToken);
    //    }

    //    public async ValueTask<bool> WriteAsync(ReadOnlyMemory<byte> data, CancellationToken cancellationToken = default)
    //    {
    //        ThrowIfDisposed();
    //        cancellationToken.ThrowIfCancellationRequested();

    //        try
    //        {
    //            if (connection is not null && State == ConnectionState.Opened)
    //            {
    //                await connection.Stream.WriteAsync(data, cancellationToken);
    //                return true;
    //            }
    //        }
    //        catch
    //        {
    //            return false;
    //        }
    //        return true;
    //    }

    //    public ValueTask<bool> OpenStreamAsync(string? domain = null, CancellationToken cancellationToken = default)
    //    {
    //        byte[] data = Encoding.UTF8.GetBytes(string.Format(streamOpenXml, domain));
    //        return WriteAsync(data, cancellationToken);
    //    }

    //    public ValueTask<bool> CloseStreamAsync(CancellationToken cancellationToken = default)
    //    {
    //        byte[] data = Encoding.UTF8.GetBytes(streamCloseXml);
    //        return WriteAsync(data, cancellationToken);
    //    }

    //    public async ValueTask<bool> SecureAsync(SslProtocols sslProtocols = SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13, CancellationToken cancellationToken = default)
    //    {
    //        ThrowIfDisposed();
    //        cancellationToken.ThrowIfCancellationRequested();

    //        if (connection is null || RemoteEndpoint is null || cancellationTokenSource is null)
    //        {
    //            throw new InvalidOperationException();
    //        }

    //        bool RemoteCertificateValidationCallback(object sender, X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
    //        {
    //            var task = eventPublisher.PublishAsync(new CertificateValidatingEventArgs(this, certificate));
    //            var result = task.IsCompleted ? task.GetAwaiter().GetResult() : task.AsTask().GetAwaiter().GetResult();

    //            return result == EventResult.Continue;
    //        }

    //        await StopReadingAsync().ConfigureAwait(false);
    //        try
    //        {
    //            var options = new SslClientAuthenticationOptions();
    //            options.TargetHost = RemoteEndpoint.Host;
    //            options.EnabledSslProtocols = sslProtocols;
    //            options.CertificateRevocationCheckMode = X509RevocationMode.Online;
    //            options.RemoteCertificateValidationCallback = RemoteCertificateValidationCallback;

    //            connection = await SslSocketConnectionFactory.SecureAsync(connection, options, cancellationToken).ConfigureAwait(false) as SocketConnection;

    //            readingTask = StartReadingAsync(cancellationTokenSource.Token);
    //            _ = eventPublisher.PublishAsync(new CertificateValidationSucceedEventArgs(this));

    //            return true;
    //        }
    //        catch (AuthenticationException e)
    //        {
    //            Reset();
    //            _ = eventPublisher.PublishAsync(new CertificateValidationFailedEventArgs(this, e.Message));
    //        }
    //        // This exception is thrown when a server doesn't support provided SslProtocols
    //        catch (Win32Exception e)
    //        {
    //            Reset();
    //            var socketError = new SocketException(e.ErrorCode);
    //            _ = eventPublisher.PublishAsync(new ConnectionErrorEventArgs(this, socketError.Message, socketError.ErrorCode));
    //        }
    //        catch (IOException)
    //        {
    //        }

    //        return false;
    //    }

    //    private ValueTask<SocketConnection> OpenSocketConnectionAsync(ConnectionEndpoint endpoint, CancellationToken cancellationToken = default)
    //    {
    //        var addressFamily = endpoint.Address.AddressFamily;
    //        var protocolType = ProtocolType.Tcp;
    //        var socketType = SocketType.Stream;
    //        var connectionFactory = new SocketConnectionFactory(addressFamily, socketType, protocolType);

    //        return connectionFactory.ConnectAsync(new IPEndPoint(endpoint.Address, endpoint.Port), cancellationToken);
    //    }

    //    private async ValueTask<ConnectionEndpoint?> ResolveRemoteEndpointAsync()
    //    {
    //        //IPHostEntry? remoteHostEntry = null;
    //        //string hostOrAddress = connectionSettings.UseHostFromAccountAddress ? AccountAddress.Host : connectionSettings.Host;

    //        //_ = dnsResolving.PublishAsync(new DnsResolvingEventArgs(this, hostOrAddress));
    //        //try
    //        //{
    //        //    remoteHostEntry = await Dns.GetHostEntryAsync(AccountAddress.Host).ConfigureAwait(false);
    //        //    _ = dnsResolutionSucceed.PublishAsync(new DnsResolutionSucceedEventArgs(this, hostOrAddress, remoteHostEntry.AddressList));
    //        //    return new ConnectionEndpoint(remoteHostEntry.AddressList.First(), remoteHostEntry.HostName, connectionSettings.Port);
    //        //}
    //        //catch (SocketException e)
    //        //{
    //        //    Reset();
    //        //    _ = dnsResolutionFailed.PublishAsync(new DnsResolutionFailedEventArgs(this, hostOrAddress, e.Message, e.ErrorCode));
    //        //}

    //        return null;
    //    }

    //    private ValueTask<ConnectionEndpoint?> ResolveLocalEndpointAsync()
    //    {
    //        var hostName = Dns.GetHostName();
    //        var hostEntry = Dns.GetHostEntry(hostName);
    //        var ipEndpoint = connection?.Socket.LocalEndPoint as IPEndPoint;

    //        if (hostName is not null && hostEntry is not null && ipEndpoint is not null)
    //        {
    //            return ValueTask.FromResult<ConnectionEndpoint?>(new ConnectionEndpoint(ipEndpoint.Address, hostName, ipEndpoint.Port));
    //        }

    //        return ValueTask.FromResult<ConnectionEndpoint?>(null);
    //    }

    //    private async Task StartReadingAsync(CancellationToken cancellationToken = default)
    //    {
    //        if (connection is null)
    //        {
    //            throw new InvalidOperationException();
    //        }

    //        byte[] buffer = ArrayPool<byte>.Shared.Rent(connection.Socket.ReceiveBufferSize);
    //        try
    //        {
    //            while (connection.Socket.Connected)
    //            {
    //                int bytesRead = await connection.Stream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);

    //                if (bytesRead > 0)
    //                {
    //                    parser.Parse(new ReadOnlySpan<byte>(buffer, 0, bytesRead));
    //                }
    //            }
    //        }
    //        catch (SocketException e)
    //        {
    //            Reset();
    //            _ = eventPublisher.PublishAsync(new ConnectionErrorEventArgs(this, e.Message, e.ErrorCode));
    //        }
    //        catch (IOException)
    //        {
    //            Reset();
    //            SetState(ConnectionState.Closed);
    //        }
    //        catch (OperationCanceledException)
    //        {
    //            Reset();
    //            SetState(ConnectionState.Closed);
    //        }
    //        catch
    //        {
    //        }
    //        finally
    //        {
    //            ArrayPool<byte>.Shared.Return(buffer, true);
    //        }
    //    }

    //    private ValueTask StopReadingAsync()
    //    {
    //        cancellationTokenSource?.Cancel();

    //        if (readingTask is not null)
    //        {
    //            return new ValueTask(readingTask);
    //        }

    //        return default;
    //    }

    //    private void SetState(ConnectionState state)
    //    {
    //        State = state;
    //        _ = eventPublisher.PublishAsync(new ConnectionStateChangedEventArgs(this, state));
    //    }

    //    private void Reset()
    //    {
    //        _ = StopReadingAsync();

    //        cancellationTokenSource?.Dispose();
    //        connection?.Dispose();
    //        parser.Reset();

    //        cancellationTokenSource = null;
    //        readingTask = null;
    //        connection = null;

    //        State = ConnectionState.Closed;
    //    }

    //    private void ThrowIfDisposed()
    //    {
    //        if (disposed)
    //        {
    //            throw new ObjectDisposedException(nameof(XmppSocketConnection));
    //        }
    //    }

    //    private void EventParserStreamClosed(object sender)
    //    {
    //        _ = eventPublisher.PublishAsync(new XmppStreamClosedEventArgs(this));
    //        _ = CloseStreamAsync();
    //    }

    //    private void EventParserStreamOpened(object sender)
    //    {
    //        _ = eventPublisher.PublishAsync(new XmppStreamOpenedEventArgs(this));
    //    }

    //    private void EventParserElementParsed(object sender, XmppParserElementParsedEventArgs e)
    //    {
    //        _ = eventPublisher.PublishAsync(new XmppElementReceivedEventArgs(this, e.Element));
    //    }

    //    private void EventParserParsingFailed(object sender, XmppParserFailedEventArgs e)
    //    {
    //        _ = eventPublisher.PublishAsync(new XmppParsingFailedEventArgs(this, e.Exception));
    //    }

    //    public override ValueTask<bool> OpenAsync(bool rejoinChannels = true, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public override ValueTask<bool> CloseAsync(bool closeStream = true, CancellationToken cancellationToken = default)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}
}