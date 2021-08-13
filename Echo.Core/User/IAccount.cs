using System;

namespace Echo.Core.User
{
    public interface IAccount
    {
        Guid Id { get; }
        XmppUri Address { get; }
        string Password { get; }
        bool IsAuthenticated { get; }
        bool IsMetaAccount { get; }
        bool IsLinked { get; }
        Presence Presence { get; }
    }
}