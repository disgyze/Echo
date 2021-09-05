using System;

namespace Echo.Core.User
{
    public sealed class AccountRemovedEventArgs : EventArgs
    {
        public IAccount Account { get; }

        public AccountRemovedEventArgs(IAccount account)
        {
            Account = account;
        }
    }
}