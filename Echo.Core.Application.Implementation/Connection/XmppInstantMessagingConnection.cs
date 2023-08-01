using System;
using System.Threading;
using System.Threading.Tasks;
using Echo.Core.Configuration;
using Echo.Core.User;
using Echo.Xmpp.Connections;

namespace Echo.Core.Connections
{
    //public sealed class XmppInstantMessagingConnection : XmppConnection
    //{
    //    Xmpp.Connections.XmppCoreConnection? connection = null;
    //    Timer? reconnectionTimer = null;
    //    ConnectionSettings settings = ConnectionSettings.Default;
    //    ConnectionEndpoint? localEndpoint;
    //    ConnectionEndpoint? remoteEndpoint;
    //    InstantMessagingConnectionState state = InstantMessagingConnectionState.Closed;
    //    bool isEncrypted = false;
    //    bool isAuthenticated = false;

    //    public override Guid Id { get; }
    //    public override Account Account { get; }
    //    public override ConnectionEndpoint? LocalEndpoint => localEndpoint;
    //    public override ConnectionEndpoint? RemoteEndpoint => remoteEndpoint;
    //    public override InstantMessagingConnectionState State => state;
    //    public override bool IsEncrypted => isEncrypted;
    //    public override bool IsAuthenticated => isAuthenticated;

    //    public XmppInstantMessagingConnection(Account account)
    //    {
    //        Id = Guid.NewGuid();
    //        Account = account ?? throw new ArgumentNullException(nameof(account));
    //    }

    //    public override ValueTask OpenAsync(CancellationToken cancellationToken = default)
    //    {
    //        string? host = settings.UseHostFromAccountAddress ? Account.Address.Host : settings.Host;

    //        if (host is null)
    //        {
    //            throw new InvalidOperationException("Host cannot be null");
    //        }

    //        throw new NotImplementedException();
    //    }

    //    public override ValueTask CloseAsync(bool closeStream = true, CancellationToken cancellationToken = default)
    //    {
    //        if (connection is Xmpp.Connections.XmppCoreConnection tempConnection)
    //        {
    //            return tempConnection.CloseAsync(closeStream, cancellationToken);
    //        }
    //        return ValueTask.CompletedTask;
    //    }

    //    private void EventXmppClientConnected(object sender, EventArgs e)
    //    {
    //        state = InstantMessagingConnectionState.Open;
    //        reconnectionTimer?.Dispose();
    //        reconnectionTimer = null;
    //    }

    //    private void EventXmppClientConnecting(object sender, EventArgs e)
    //    {
    //        state = InstantMessagingConnectionState.Opening;
    //    }

    //    private void EventXmppClientDisconnected(object sender, EventArgs e)
    //    {
    //        state = InstantMessagingConnectionState.Closed;
    //    }
    //}
}