using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.User
{
    public abstract class AccountStore
    {
        public abstract ValueTask<bool> SaveAccountAsync(Account account, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> UpdateAccountAsync(Account account, AccountChangeKind changeKind, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> DeleteAccountAsync(Account account, CancellationToken cancellationToken = default);
        public abstract ValueTask<Account?> GetAccountAsync(XmppAddress address, CancellationToken cancellationToken = default);
        public abstract IAsyncEnumerable<Account> GetAccountsAsync();
    }
}