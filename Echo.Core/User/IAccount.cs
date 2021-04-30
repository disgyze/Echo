using System;
using Echo.Xmpp;

namespace Echo.Core.User
{
    public interface IAccount
    {
        Guid Id { get; }
        XmppAddress Address { get; }
        bool IsMetaAccount { get; }
        bool IsLinked { get; }
        bool IsAuthenticated { get; }
        Presence Presence { get; }
    }
}