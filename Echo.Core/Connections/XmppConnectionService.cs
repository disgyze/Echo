using System;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Echo.Core.User;
using Echo.Xmpp.Connections;
using Echo.Xmpp.ElementModel;

namespace Echo.Core.Connections
{
    public sealed class XmppConnectionService
    {
        XmppConnection connection;
        XmppStanzaRequestFactory requestFactory;

        public Guid Id { get; }
        public Account Account { get; }
        public ConnectionEndpoint? LocalEndpoint { get; }
        public ConnectionEndpoint? RemoteEndpoint { get; }
        public XmppConnectionServiceState State { get; }
        public bool IsEncrypted { get; }

        internal XmppConnectionService(Account account)
        {
            Id = Guid.NewGuid();
            Account = account ?? throw new ArgumentNullException(nameof(account));
        }

        public ValueTask<bool> OpenAsync(bool forceReconnect = false, bool rejoinChannels = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> CloseAsync(bool closeStream = true, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask<bool> SendAsync(XElement element, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public ValueTask<TStanza> QueryAsync<TStanza>(TStanza stanza, TimeSpan timeout = default, CancellationToken cancellationToken = default) where TStanza : XmppStanza
        {
            throw new NotImplementedException();
        }
    }
}