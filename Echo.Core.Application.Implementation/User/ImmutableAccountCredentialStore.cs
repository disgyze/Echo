using System;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.User
{
    public sealed class ImmutableAccountCredentialStore : AccountCredentialStore
    {
        ImmutableArray<AccountCredential> credentialCollection;

        public ImmutableAccountCredentialStore(ImmutableArray<AccountCredential>? credentialCollection = null)
        {
            this.credentialCollection = credentialCollection ?? ImmutableArray<AccountCredential>.Empty;
        }

        public override ValueTask<bool> SaveAccountCredentialAsync(AccountCredential credential, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (credential is null)
            {
                throw new ArgumentNullException(nameof(credential));
            }

            static ImmutableArray<AccountCredential> Save(ImmutableArray<AccountCredential> collection, AccountCredential credential) =>
                collection.FirstOrDefault(otherCredential => otherCredential.AccountAddress == credential.AccountAddress) is null ? collection.Add(credential) : collection;

            return ValueTask.FromResult(ImmutableInterlocked.Update(ref credentialCollection, Save, credential));
        }

        public override ValueTask<bool> UpdateAccountCredentialAsync(AccountCredential credential, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (credential is null)
            {
                throw new ArgumentNullException(nameof(credential));
            }

            static ImmutableArray<AccountCredential> Update(ImmutableArray<AccountCredential> collection, AccountCredential credential) =>
                collection.FirstOrDefault(otherCredential => otherCredential.AccountAddress == credential.AccountAddress) is AccountCredential otherCredential ? collection.Replace(otherCredential, credential) : collection;

            return ValueTask.FromResult(ImmutableInterlocked.Update(ref credentialCollection, Update, credential));
        }

        public override ValueTask<bool> DeleteAccountCredentialAsync(AccountCredential credential, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (credential is null)
            {
                throw new ArgumentNullException(nameof(credential));
            }

            static ImmutableArray<AccountCredential> Delete(ImmutableArray<AccountCredential> collection, AccountCredential credential) =>
                collection.FirstOrDefault(otherCredential => otherCredential.AccountAddress == credential.AccountAddress) is AccountCredential otherCredential ? collection.Remove(otherCredential) : collection;

            return ValueTask.FromResult(ImmutableInterlocked.Update(ref credentialCollection, Delete, credential));
        }

        public override ValueTask<AccountCredential?> GetAccountCredentialAsync(XmppAddress accountAddress, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (accountAddress is null)
            {
                throw new ArgumentNullException(nameof(accountAddress));
            }

            return ValueTask.FromResult(credentialCollection.FirstOrDefault(credential => credential.AccountAddress == accountAddress));
        }

        public override ValueTask<AccountCredential?> GetAccountCredentialAsync(Account account, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (account is null)
            {
                throw new ArgumentNullException(nameof(account));
            }

            return ValueTask.FromResult(credentialCollection.FirstOrDefault(credential => credential.AccountAddress == account.Address));
        }
    }
}