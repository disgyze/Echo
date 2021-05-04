using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Xml.Linq;
using Echo.Core.Configuration.Connection;
using Echo.Core.Messaging;
using Echo.Core.UI;
using Echo.Core.User;
using Echo.Foundation;
using Echo.Networking;
using Echo.Xmpp.Parser;

namespace Echo.Core.Client
{
    using Timer = System.Timers.Timer;

    public sealed class XmppClient : IXmppClient, IDisposable, IAsyncDisposable
    {
        bool disposed = false;
        Timer reconnectionTimer = null;
        ConnectionSettings connectionSettings = null;
        DisposableContainer eventSubscriptionContainer = null;
        IXmppParser parser = null;
        IEventPublisher<ClientConnectedEventArgs> clientConnected = null;
        IEventPublisher<ClientConnectingEventArgs> clientConnecting = null;
        IEventPublisher<ClientDisconnectedEventArgs> clientDisconnected = null;
        IEventPublisher<ClientConnectionErrorEventArgs> clientConnectionError = null;
        IEventPublisher<ClientConnectionFailedEventArgs> clientConnectionFailed = null;
        IEventSubscriber<ConnectionSettingsChangedEventArgs> connectionSettingsChanged = null;

        public Guid Id { get; private set; }
        public ConnectionEndpoint LocalEndpoint { get; private set; }
        public ConnectionEndpoint RemoteEndpoint { get; private set; }
        public EncryptionProtocol Encryption { get; private set; }
        public IAccount Account { get; private set; }
        public IWindow Window { get; private set; }
        public bool IsConnecting { get; private set; } = false;
        public bool IsConnected { get; private set; } = false;
        public bool IsEncrypting { get; private set; } = false;
        public bool IsEncrypted { get; private set; } = false;

        public XmppClient(IAccount account, IWindow window, IChannelManager channelManager, IXmppParser parser, IClientSocket socket, ConnectionSettings connectionSettings)
        {
            Id = Guid.NewGuid();
            Account = account;
            Window = window;

            this.parser = parser;
            this.connectionSettings = connectionSettings;

            reconnectionTimer = new Timer();
            reconnectionTimer.Elapsed += EventReconnectionTimerTick;
        }

        public void Dispose()
        {
            if (!disposed)
            {
                eventSubscriptionContainer?.Dispose();
                reconnectionTimer?.Dispose();
                parser?.Cancel();

                eventSubscriptionContainer = null;
                reconnectionTimer = null;
                parser = null;
            }
            disposed = true;
        }

        public ValueTask DisposeAsync()
        {
            Dispose();
            return default;
        }

        public Task<bool> ConnectAsync(bool rejoinChannels = true, CancellationToken cancellationToken = default)
        {
            //var host = connectionSettings.UseHostFromAccountAddress ? Account.Address.Host : connectionSettings.Host;
            //var port = connectionSettings.Port;

            //if (socket == null)
            //{
            //    socket = new ClientSocket(); 
            //}
            //var connectionResult = await socket.ConnectAsync(new DnsEndPoint(RemoteEndpoint.Host, RemoteEndpoint.Port), default, default);
            throw new NotImplementedException();
        }

        public Task<bool> DisconnectAsync(bool closeStream = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> SendAsync(XElement element, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> StartStreamAsync(string domain = null, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CloseStreamAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        private Task<EventResult> EventConnectionSettingsChanged(object sender, ConnectionSettingsChangedEventArgs e)
        {
            if (connectionSettings == e.OldSettings)
            {
                connectionSettings = e.NewSettings;
            }
            return Task.FromResult(EventResult.Continue);
        }

        private void EventReconnectionTimerTick(object sender, ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void EventClientSocketStateChanged(object sender, ClientSocketStateChangedEventArgs e)
        {
            switch (e.State)
            {
                case ClientSocketState.Connected:
                {
                    IsConnected = true;
                    IsConnecting = false;
                    clientConnected.Publish(new ClientConnectedEventArgs(this)); 
                    break;
                }

                case ClientSocketState.Connecting:
                {
                    IsConnected = false;
                    IsConnecting = true;
                    clientConnecting.Publish(new ClientConnectingEventArgs(this)); 
                    break;
                }

                case ClientSocketState.Disconnected:
                {
                    IsConnected = false;
                    IsConnecting = false;
                    clientDisconnected.Publish(new ClientDisconnectedEventArgs(this)); 
                    break;
                }
            }
        }
    }
}