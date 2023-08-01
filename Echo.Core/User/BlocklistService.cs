using System;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Echo.Core.Connections;
using Echo.Xmpp.Connections;

namespace Echo.Core.User
{
    public sealed class BlocklistService
    {
        XmppConnectionService connection;

        public BlocklistService(XmppConnectionService connection)
        {
            this.connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }

        public Task<bool> BlockAsync(XmppAddress address, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnblockAsync(XmppAddress address, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UnblockAllAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task<ImmutableArray<XmppAddress>> GetBlockedAddressesAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
}