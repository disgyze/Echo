using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Echo.Core.Extensibility.Eventing;

namespace Echo.Core.User
{
    //public sealed class ImmutableAccountStore : AccountStore
    //{
    //    ImmutableArray<Account> accountCollection;
    //    Event<AccountAddedEventArgs> accountAdded;
    //    Event<AccountRemovedEventArgs> accountRemoved;
    //    Event<AccountChangedEventArgs> accountChanged;

    //    public ImmutableAccountStore(EventChannel eventChannel, ImmutableArray<Account>? accountCollection = null)
    //    {
    //        if (eventChannel is null)
    //        {
    //            throw new ArgumentNullException(nameof(eventChannel));
    //        }

    //        this.accountCollection = accountCollection ?? ImmutableArray<Account>.Empty;

    //        accountAdded = eventChannel.GetEvent<AccountAddedEventArgs>();
    //        accountRemoved = eventChannel.GetEvent<AccountRemovedEventArgs>();
    //        accountChanged = eventChannel.GetEvent<AccountChangedEventArgs>();
    //    }

    //    public override ValueTask<bool> SaveAccountAsync(Account account, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        if (account is null)
    //        {
    //            throw new ArgumentNullException(nameof(account));
    //        }

    //        static ImmutableArray<Account> Save(ImmutableArray<Account> collection, Account account) =>
    //            collection.FirstOrDefault(otherAccount => otherAccount.Address.EqualsBare(account.Address)) is null ? collection.Add(account) : collection;

    //        if (ImmutableInterlocked.Update(ref accountCollection, Save, account))
    //        {
    //            _ = accountAdded.PublishAsync(new AccountAddedEventArgs(account));
    //            return ValueTask.FromResult(true);
    //        }

    //        return ValueTask.FromResult(false);
    //    }

    //    public override ValueTask<bool> UpdateAccountAsync(Account account, AccountChangeKind changeKind, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        if (account is null)
    //        {
    //            throw new ArgumentNullException(nameof(account));
    //        }

    //        Account? oldAccount = accountCollection.FirstOrDefault(otherAccount => otherAccount.Address.EqualsBare(account.Address));

    //        if (oldAccount is null)
    //        {
    //            return ValueTask.FromResult(false);
    //        }

    //        try
    //        {
    //            ImmutableInterlocked.Update(ref accountCollection, (collection, account) => collection.Replace(oldAccount, account), account);
    //            _ = accountChanged.PublishAsync(new AccountChangedEventArgs(account, oldAccount, changeKind));
    //        }
    //        catch (ArgumentException)
    //        {
    //            return ValueTask.FromResult(false);
    //        }

    //        return ValueTask.FromResult(true);
    //    }

    //    public override ValueTask<bool> DeleteAccountAsync(Account account, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        if (account is null)
    //        {
    //            throw new ArgumentNullException(nameof(account));
    //        }

    //        static ImmutableArray<Account> Delete(ImmutableArray<Account> collection, Account account) =>
    //            collection.FirstOrDefault(otherAccount => otherAccount.Address.EqualsBare(account.Address)) is not null ? collection.Remove(account) : collection;

    //        if (ImmutableInterlocked.Update(ref accountCollection, Delete, account))
    //        {
    //            _ = accountRemoved.PublishAsync(new AccountRemovedEventArgs(account));
    //            return ValueTask.FromResult(true);
    //        }

    //        return ValueTask.FromResult(false);
    //    }

    //    public override ValueTask<Account?> GetAccountAsync(XmppAddress address, CancellationToken cancellationToken = default)
    //    {
    //        cancellationToken.ThrowIfCancellationRequested();

    //        if (address is null)
    //        {
    //            throw new ArgumentNullException(nameof(address));
    //        }

    //        return ValueTask.FromResult(accountCollection.FirstOrDefault(account => account.Address.EqualsBare(address)));
    //    }

    //    public override async IAsyncEnumerable<Account> GetAccountsAsync()
    //    {
    //        foreach (var account in accountCollection)
    //        {
    //            yield return account;
    //        }
    //        await Task.CompletedTask.ConfigureAwait(false);
    //    }
    //}
}