using System;
using System.Collections.Immutable;
using System.Linq;
using Echo.Foundation;

namespace Echo.Core.Connections
{
    public sealed class XmppConnectionManager : IXmppConnectionManager
    {
        IXmppConnection? activeConnection = null;
        ImmutableArray<IXmppConnection> connectionList = ImmutableArray<IXmppConnection>.Empty;

        IEvent<ActiveConnectionChangedEventArgs> activeConnectionChanged;

        public int Count => connectionList.Length;
        public IXmppConnection? ActiveConnection => activeConnection;

        public XmppConnectionManager(IEvent<ActiveConnectionChangedEventArgs> activeConnectionChanged)
        {
            this.activeConnectionChanged = activeConnectionChanged ?? throw new ArgumentNullException(nameof(activeConnectionChanged));
        }

        public IXmppConnection? GetConnection(int connectionIndex)
        {
            var temp = connectionList;
            return connectionIndex >= 0 && connectionIndex < temp.Length ? temp[connectionIndex] : null;
        }

        public IXmppConnection? GetConnection(XmppUri accountUri)
        {
            return accountUri != null ? connectionList.FirstOrDefault(connection => XmppUri.Equals(connection.Account.Address, accountUri, XmppUriComparison.Bare)) : null;
        }

        public void Add(IXmppConnection connection)
        {
            ImmutableInterlocked.Update(ref connectionList, connectionList => connectionList.Add(connection));
        }

        public void Remove(IXmppConnection connection)
        {
            ImmutableInterlocked.Update(ref connectionList, connectionList => connectionList.Remove(connection));
        }
                
        public void SetActiveConnection(IXmppConnection? connection)
        {
            if (activeConnection == connection)
            {
                return;
            }

            var oldConnection = activeConnection;
            activeConnection = connection;

            activeConnectionChanged.Publish(new ActiveConnectionChangedEventArgs(oldConnection, activeConnection));
        }
    }
}