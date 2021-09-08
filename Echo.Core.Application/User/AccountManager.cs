using System;

namespace Echo.Core.User
{
    public abstract class AccountManager : IAccountManager
    {
        public abstract int Count { get; }

        public abstract Account? GetAccount(int accountIndex);
        public abstract Account? GetAccount(Guid accountId);
        public abstract Account? GetAccount(XmppAddress accountAddress);
        public abstract void Add(Account account);
        public abstract void Remove(Account account);
        public abstract void ShowAccountRegistrationUI();
        public abstract void ShowAccountUI(XmppAddress accountAddress);

        IAccount? IAccountManager.GetAccount(int accountIndex) => GetAccount(accountIndex);
        IAccount? IAccountManager.GetAccount(Guid accountId) => GetAccount(accountId);
        IAccount? IAccountManager.GetAccount(XmppAddress accountAddress) => GetAccount(accountAddress);
    }
}