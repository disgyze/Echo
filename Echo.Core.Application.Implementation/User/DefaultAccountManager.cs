using System;
using System.Collections.Immutable;
using System.Linq;
using Echo.Core.Extensibility;

namespace Echo.Core.User
{
    public sealed class DefaultAccountManager : AccountManager
    {
        ImmutableArray<Account> accountList = ImmutableArray<Account>.Empty;
        IAccountPresenter accountPresenter;
        IAccountRegistrationPresenter accountRegistrationPresenter;
        IEventPublisher<AccountAddedEventArgs> accountAdded;
        IEventPublisher<AccountRemovedEventArgs> accountRemoved;

        public override int Count => accountList.Length;

        public DefaultAccountManager(IAccountPresenter accountPresenter,
                                     IAccountRegistrationPresenter accountRegistrationPresenter,
                                     IEventPublisher<AccountAddedEventArgs> accountAdded,
                                     IEventPublisher<AccountRemovedEventArgs> accountRemoved)
        {
            this.accountPresenter = accountPresenter ?? throw new ArgumentNullException(nameof(accountPresenter));
            this.accountRegistrationPresenter = accountRegistrationPresenter ?? throw new ArgumentNullException(nameof(accountRegistrationPresenter));
            this.accountAdded = accountAdded ?? throw new ArgumentNullException(nameof(accountAdded));
            this.accountRemoved = accountRemoved ?? throw new ArgumentNullException(nameof(accountRemoved));
        }

        public override Account? GetAccount(int accountIndex)
        {
            var copy = accountList;
            return accountIndex >= 0 && accountIndex < copy.Length ? copy[accountIndex] : null;
        }

        public override Account? GetAccount(Guid accountId)
        {
            return accountList.FirstOrDefault(account => account.Id == accountId);
        }

        public override Account? GetAccount(XmppAddress accountAddress)
        {
            return accountList.FirstOrDefault(account => account.Address.EqualsBare(accountAddress));
        }

        public override void Add(Account account)
        {
            if (ImmutableInterlocked.Update(ref accountList, accountList => accountList.Add(account)))
            {
                accountAdded.PublishAsync(new AccountAddedEventArgs(account));
            }
        }

        public override void Remove(Account account)
        {
            if (ImmutableInterlocked.Update(ref accountList, accountList => accountList.Remove(account)))
            {
                accountRemoved.PublishAsync(new AccountRemovedEventArgs(account));
            }
        }

        public override void ShowAccountRegistrationUI()
        {
            accountRegistrationPresenter.ShowUI();
        }

        public override void ShowAccountUI(XmppAddress accountAddress)
        {
            accountPresenter.ShowUI(accountAddress);
        }
    }
}