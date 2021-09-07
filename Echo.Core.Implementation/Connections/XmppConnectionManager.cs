using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Echo.Core.Extensibility;
using Echo.Core.User;

namespace Echo.Core.Connections
{
    public sealed class XmppConnectionManager : IXmppConnectionManager
    {
        IXmppConnection? activeConnection = null;
        IXmppConnectionFactory connectionFactory;
        ImmutableArray<IXmppConnection> connectionList = ImmutableArray<IXmppConnection>.Empty;
        IEventPublisher<ConnectionAddedEventArgs> connectionAdded;
        IEventPublisher<ConnectionRemovedEventArgs> connectionRemoved;
        IEventPublisher<ActiveConnectionChangedEventArgs> activeConnectionChanged;

        public int Count => connectionList.Length;
        public IXmppConnection? ActiveConnection => activeConnection;

        public XmppConnectionManager(IXmppConnectionFactory connectionFactory,
                                     IEventPublisher<ConnectionAddedEventArgs> connectionAdded,
                                     IEventPublisher<ConnectionRemovedEventArgs> connectionRemoved,
                                     IEventPublisher<ActiveConnectionChangedEventArgs> activeConnectionChanged,
                                     IEventSubscriber<AccountAddedEventArgs> accountAdded,
                                     IEventSubscriber<AccountRemovedEventArgs> accountRemoved)
        {
            this.connectionFactory = connectionFactory ?? throw new ArgumentNullException(nameof(connectionFactory));
            this.connectionAdded = connectionAdded ?? throw new ArgumentNullException(nameof(connectionAdded));
            this.connectionRemoved = connectionRemoved ?? throw new ArgumentNullException(nameof(connectionRemoved));
            this.activeConnectionChanged = activeConnectionChanged ?? throw new ArgumentNullException(nameof(activeConnectionChanged));

            accountAdded.Subscribe(EventAccountAdded);
            accountRemoved.Subscribe(EventAccountRemoved);
        }

        public IXmppConnection? GetConnection(int connectionIndex)
        {
            var temp = connectionList;
            return connectionIndex >= 0 && connectionIndex < temp.Length ? temp[connectionIndex] : null;
        }

        public IXmppConnection? GetConnection(XmppAddress accountAddress)
        {
            return connectionList.FirstOrDefault(connection => connection.Account.Address.EqualsBare(accountAddress));
        }

        public bool SetActiveConnection(IXmppConnection? connection)
        {
            if (activeConnection == connection)
            {
                return false;
            }

            var oldConnection = activeConnection;
            activeConnection = connection;

            activeConnectionChanged.PublishAsync(new ActiveConnectionChangedEventArgs(oldConnection, activeConnection));

            return true;
        }

        private void Add(IXmppConnection connection)
        {
            if (ImmutableInterlocked.Update(ref connectionList, connectionList => connectionList.Add(connection)))
            {
                connectionAdded.PublishAsync(new ConnectionAddedEventArgs(connection));
            }
        }

        private void Remove(IXmppConnection connection)
        {
            if (ImmutableInterlocked.Update(ref connectionList, connectionList => connectionList.Remove(connection)))
            {
                connectionRemoved.PublishAsync(new ConnectionRemovedEventArgs(connection));
            }
        }

        private ValueTask<EventResult> EventAccountAdded(AccountAddedEventArgs e)
        {
            Add(connectionFactory.Create(e.Account));
            return ValueTask.FromResult(EventResult.Continue);
        }

        private ValueTask<EventResult> EventAccountRemoved(AccountRemovedEventArgs e)
        {
            var connection = GetConnection(e.Account.Address);

            if (connection != null)
            {
                Remove(connection);
            }

            return ValueTask.FromResult(EventResult.Continue);
        }
    }
}