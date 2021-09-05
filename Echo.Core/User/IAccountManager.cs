using System;

namespace Echo.Core.User
{
    public interface IAccountManager
    {
        int Count { get; }

        IAccount? GetAccount(int accountIndex);
        IAccount? GetAccount(Guid accountId);
        IAccount? GetAccount(XmppUri accountAddress);
        IAccount? CreateAccount(XmppUri address, string? password = null);
        IAccount? UpdateAccount(IAccount account);

        void ShowAccountUI(XmppUri accountAddress);
        void ShowAccountRegistrationUI();
    }
}