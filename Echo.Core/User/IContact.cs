using System;
using System.Collections.Generic;
using Echo.Xmpp;

namespace Echo.Core.User
{
    public interface IContact
    {
        Guid Id { get; }
        XmppAddress Address { get; }
        bool IsMetaContact { get; }
        bool IsLinked { get; }
        IAccount Account { get; }
        IReadOnlyList<string> Groups { get; }
    }
}