using System;

namespace Echo.Core.User
{
    public interface IAccount
    {
        Guid Id { get; }
        XmppUri Address { get; }
        bool IsMetaAccount { get; }
        bool IsLinked { get; }
        bool IsAuthenticated { get; }
        Presence Presence { get; }
    }
}