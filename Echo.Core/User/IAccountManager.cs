using System;

namespace Echo.Core.User
{
    public interface IAccountManager
    {
        int Count { get; }

        IAccount? GetAccount(int accountIndex);
        IAccount? GetAccount(Guid accountId);
        IAccount? GetAccount(XmppAddress accountAddress);
        IAccount? CreateAccount(XmppAddress address, string? password = null);
        IAccount? UpdateAccount(IAccount account);

        void ShowAccountUI(XmppAddress accountAddress);
        void ShowAccountRegistrationUI();
    }
}