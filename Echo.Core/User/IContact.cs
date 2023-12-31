﻿using System;
using System.Collections.Generic;

namespace Echo.Core.User
{
    public interface IContact
    {
        Guid Id { get; }
        XmppAddress Address { get; }
        IAccount Account { get; }
        IReadOnlyList<string> Groups { get; }
    }
}