﻿using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Echo.Core.Configuration.Connection;
using Echo.Core.Extensibility;
using Echo.Core.UI;
using Echo.Core.User;
using Echo.Foundation;
using Echo.Networking;
using Echo.Xmpp.Parser;

namespace Echo.Core.Connections
{
    public sealed class DefaultXmppConnection : IXmppConnection, IDisposable, IAsyncDisposable
    {
        bool disposed = false;
        Guid id = default;
        ConnectionEndPoint? localEndPoint = null;
        ConnectionEndPoint? remoteEndPoint = null;
        SecurityState securityState = SecurityState.None;
        ConnectionState connectionState = ConnectionState.Closed;
        EncryptionProtocol encryptionProtocol = EncryptionProtocol.None;
        IAccount account = null!;
        IWindow window = null!;
        IXmppParser? parser = null!;
        NameResolver nameResolver = null!;
        NetworkStreamReader? streamReader = null;
        NetworkStreamWriter? streamWriter = null;
        SocketConnection? connection = null;
        ConnectionSettings connectionSettings = null!;
        CancellationTokenSource cancellationTokenSource = null;

        IEventPublisher<ConnectionErrorEventArgs> connectionError = null!;
        IEventPublisher<ConnectionFailedEventArgs> connectionFailed = null!;
        IEventPublisher<ConnectionStateChangedEventArgs> connectionStateChanged = null!;
        IEventPublisher<DnsResolutionFailedEventArgs> dnsResolutionFailed = null!;
        IEventPublisher<DnsResolvingEventArgs> dnsResolving = null!;
        IEventPublisher<DnsResolvedEventArgs> dnsResolved = null!;

        public Guid Id
        {
            get
            {
                ThrowIfDisposed();
                return id;
            }
        }

        public ConnectionEndPoint? LocalEndPoint
        {
            get
            {
                ThrowIfDisposed();
                return localEndPoint;
            }
        }

       public ConnectionEndPoint? RemoteEndPoint
        {
            get
            {
                ThrowIfDisposed();
                return remoteEndPoint;
            }
        }

        public SecurityState SecurityState
        {
            get
            {
                ThrowIfDisposed();
                return securityState;
            }
        }

        public ConnectionState ConnectionState
        {
            get
            {
                ThrowIfDisposed();
                return connectionState;
            }
        }

        public EncryptionProtocol EncryptionProtocol
        {
            get
            {
                ThrowIfDisposed();
                return encryptionProtocol;
            }
        }

        public IAccount Account
        {
            get
            {
                ThrowIfDisposed();
                return account;
            }
        }

        public IWindow Window
        {
            get
            {
                ThrowIfDisposed();
                return window;
            }
        }

        public bool IsDisposed => throw new NotImplementedException();

        public IXmppStream Stream => throw new NotImplementedException();

        public DefaultXmppConnection(IAccount account, IWindow window, IXmppParser parser)
        {

        }

        public async Task<bool> OpenAsync(bool forceReconnect = false, CancellationToken cancellationToken = default)
        {
            ThrowIfDisposed();

            if (ConnectionState != ConnectionState.Closed || cancellationToken.IsCancellationRequested)
            {
                return false;
            }

            cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            try
            {
                string host = connectionSettings.UseHostFromAccountAddress ? account.Address.Host : connectionSettings.Host!;

                IPHostEntry dnsHostEntry = null!;
                try
                {
                    dnsResolving.PublishAsync(new DnsResolvingEventArgs(this, host));
                    dnsHostEntry = await nameResolver.ResolveAsync(host, cancellationTokenSource.Token);
                    dnsResolved.PublishAsync(new DnsResolvedEventArgs(dnsHostEntry.AddressList));
                }
                catch (SocketException e)
                {
                    dnsResolutionFailed.PublishAsync(new DnsResolutionFailedEventArgs(this, host, e.Message, e.ErrorCode));
                    return false;
                }

                IPEndPoint connectionEndPoint = new IPEndPoint(dnsHostEntry.AddressList.FirstOrDefault(), connectionSettings.Port);
                ConnectionFactory connectionFactory = new SocketConnectionFactory(connectionEndPoint.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

                if (connectionSettings.ProxySettings != null)
                {
                    //connectionFactory = new ProxyConnectionFactory(connectionFactory, new ProxyOptions())
                }

                SetState(ConnectionState.Opening);
                connection = await connectionFactory!.OpenAsync(connectionEndPoint, cancellationTokenSource.Token);

                IPEndPoint socketLocalEndPoint = (IPEndPoint)connection.Socket.LocalEndPoint!;
                localEndPoint = new ConnectionEndPoint(socketLocalEndPoint.Address, Dns.GetHostName(), socketLocalEndPoint.Port);

                IPEndPoint socketRemoteEndPoint = (IPEndPoint)connection.Socket.RemoteEndPoint!;
                remoteEndPoint = new ConnectionEndPoint(socketRemoteEndPoint.Address, host, socketRemoteEndPoint.Port);

                SetState(ConnectionState.Opened);
                StartDataExchange(cancellationTokenSource.Token);

                return true;
            }
            catch (SocketException e)
            {
                Reset();
                await connectionFailed.PublishAsync(new ConnectionFailedEventArgs(this, e.Message, e.ErrorCode));
            }

            return false;
        }

        public Task<bool> DisconnectAsync(bool closeStream = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> SendAsync(XElement? element, CancellationToken cancellationToken = default)
        {
            if (ConnectionState != ConnectionState.Opened)
            {
                Window.Display.ShowError("Not connected");
                return ValueTask.FromResult(false);
            }

            if (element == null)
            {
                return ValueTask.FromResult(false);
            }

            streamWriter!.Write(Encoding.UTF8.GetBytes(element.ToString()));
            return ValueTask.FromResult(true);
        }

        public Task<bool> StartStreamAsync(string? domain = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CloseStreamAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        private ValueTask<IPHostEntry> ResolveAsync(string hostOrAddress, CancellationToken cancellationToken = default)
        {
            return nameResolver.ResolveAsync(hostOrAddress, cancellationToken);
        }

        private void SetState(ConnectionState state)
        {
            connectionState = state;
            connectionStateChanged.PublishAsync(new ConnectionStateChangedEventArgs(this, state));
        }

        private void StartDataExchange(CancellationToken cancellationToken = default)
        {
            void HandleData(byte[] data)
            {
                parser?.Parse(data);
            }

            void HandleError(Exception error)
            {
                if (error is SocketException socketError)
                {
                    connectionError.PublishAsync(new ConnectionErrorEventArgs(this, socketError.Message, socketError.ErrorCode));
                }
            }

            streamReader = new NetworkStreamReader((NetworkStream)connection!.Stream);
            streamWriter = new NetworkStreamWriter((NetworkStream)connection!.Stream);

            _ = streamReader.StartAsync(HandleData, HandleError, cancellationToken);
            _ = streamWriter.StartAsync(HandleError, cancellationToken);
        }

        private void StopDataExchange()
        {
            streamReader?.Dispose();
            streamWriter?.Dispose();

            streamReader = null;
            streamWriter = null;
        }

        private void Reset()
        {
            streamReader?.Dispose();
            streamReader = null;

            streamWriter?.Dispose();
            streamWriter = null;

            connection?.Dispose();
            connection = null;

            parser?.Cancel();

            securityState = SecurityState.None;
            connectionState = ConnectionState.Closed;
            encryptionProtocol = EncryptionProtocol.None;
            localEndPoint = null;
            remoteEndPoint = null;
        }

        private void ThrowIfDisposed()
        {
            if (disposed)
            {
                throw new ObjectDisposedException(nameof(DefaultXmppConnection));
            }
        }

        public Task<bool> OpenAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> CloseAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SecureAsync()
        {
            throw new NotImplementedException();
        }

        public object? GetService(Type serviceType)
        {
            throw new NotImplementedException();
        }

        public ValueTask CommandAsync(string text)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public ValueTask DisposeAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> OpenAsync(bool forceReconnect = false)
        {
            throw new NotImplementedException();
        }
    }
}