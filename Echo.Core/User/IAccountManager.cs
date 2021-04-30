using System;
using Echo.Xmpp;

namespace Echo.Core.User
{
    public interface IAccountManager
    {
        int Count { get; }

        IAccount GetAccount(int accountIndex);
        IAccount GetAccount(Guid accountId);
        IAccount GetAccount(XmppAddress accountAddress);
    }
}