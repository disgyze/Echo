using System;

namespace Echo.Core.User
{
    public sealed class AccountAddedEventArgs : EventArgs
    {
        public IAccount Account { get; }

        public AccountAddedEventArgs(IAccount account)
        {
            Account = account;
        }
    }
}