using System;
using System.Collections.Generic;

namespace Echo.Core.User
{
    public interface IContact
    {
        Guid Id { get; }
        Uri Address { get; }
        bool IsMetaContact { get; }
        bool IsLinked { get; }
        IAccount Account { get; }
        IReadOnlyList<string> Groups { get; }
    }
}