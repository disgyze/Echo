using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading.Tasks;
using Echo.Core.Extensibility.Eventing;
using Echo.Core.User;

namespace Echo.Core.Connections
{
    public sealed class XmppConnectionServiceManager
    {
        IEventPublisher<ActiveConnectionChangedEventArgs> activeConnectionChanged;
        IEventPublisher<XmppConnectionServiceAddedEventArgs> connectionAdded;
        IEventPublisher<XmppConnectionServiceConnectionRemovedEventArgs> connectionRemoved;
        ImmutableArray<XmppConnectionService> connectionCollection = ImmutableArray<XmppConnectionService>.Empty;

        public XmppConnectionService? ActiveConnection { get; private set; }
        public IReadOnlyList<XmppConnectionService> Connections => connectionCollection;

        public XmppConnectionServiceManager(
            IEventPublisher<ActiveConnectionChangedEventArgs> activeConnectionChanged, 
            IEventPublisher<XmppConnectionServiceAddedEventArgs> connectionAdded,
            IEventPublisher<XmppConnectionServiceConnectionRemovedEventArgs> connectionRemoved,
            IEventSubscriber<AccountAddedEventArgs> accountAdded,
            IEventSubscriber<AccountRemovedEventArgs> accountRemoved)
        {
            this.activeConnectionChanged = activeConnectionChanged ?? throw new ArgumentNullException(nameof(activeConnectionChanged));
            this.connectionAdded = connectionAdded ?? throw new ArgumentNullException(nameof(connectionAdded));
            this.connectionRemoved = connectionRemoved ?? throw new ArgumentNullException(nameof(connectionRemoved));

            accountAdded.Subscribe(EventAccountAdded);
            accountRemoved.Subscribe(EventAccountRemoved);
        }

        internal void SetActiveConnection(XmppConnectionService? connection)
        {
            var oldConnection = ActiveConnection;
            ActiveConnection = connection;

            _ = activeConnectionChanged.PublishAsync(new ActiveConnectionChangedEventArgs(oldConnection, connection));
        }

        internal void AddConnection(XmppConnectionService connection)
        {
            bool updated = ImmutableInterlocked.Update(ref connectionCollection, static (collection, connection) =>
            {
                return collection.Contains(connection) ? collection : collection.Add(connection);
            }, connection);

            if (updated)
            {
                _ = connectionAdded.PublishAsync(new XmppConnectionServiceAddedEventArgs(connection));
            }
        }

        internal void RemoveConnection(XmppConnectionService connection)
        {
            bool updated = ImmutableInterlocked.Update(ref connectionCollection, static (collection, connection) =>
            {
                return collection.Remove(connection);
            }, connection);

            if (updated)
            {
                _ = connectionRemoved.PublishAsync(new XmppConnectionServiceConnectionRemovedEventArgs(connection));
            }
        }

        private ValueTask<EventResult> EventAccountAdded(AccountAddedEventArgs e)
        {
            AddConnection(new XmppConnectionService(e.Account));
            return ValueTask.FromResult(EventResult.Stop);
        }

        private ValueTask<EventResult> EventAccountRemoved(AccountRemovedEventArgs e)
        {
            var collection = connectionCollection;
            var connection = connectionCollection.FirstOrDefault(connection => connection.Account == e.Account);

            if (connection is not null)
            {
                _ = connection.CloseAsync(true);
                bool updated = ImmutableInterlocked.Update(ref connectionCollection, static (collection, connection) =>
                {
                    return collection.Remove(connection);
                }, connection);

                if (updated)
                {
                    _ = connectionRemoved.PublishAsync(new XmppConnectionServiceConnectionRemovedEventArgs(connection));
                }
            }

            return ValueTask.FromResult(EventResult.Stop);
        }
    }
}