using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.User
{
    public abstract class AccountCredentialStore
    {
        public abstract ValueTask<AccountCredential?> GetAccountCredentialAsync(Account account, CancellationToken cancellationToken = default);
        public abstract ValueTask<AccountCredential?> GetAccountCredentialAsync(XmppAddress accountAddress, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> SaveAccountCredentialAsync(AccountCredential credential, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> UpdateAccountCredentialAsync(AccountCredential credential, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> DeleteAccountCredentialAsync(AccountCredential credential, CancellationToken cancellationToken = default);
    }
}