﻿using System;
using Echo.Core.Connections;

namespace Echo.Core.User
{
    public sealed class SubscriptionApprovedEventArgs : EventArgs
    {
        public IXmppConnection Connection { get; }
    }
}