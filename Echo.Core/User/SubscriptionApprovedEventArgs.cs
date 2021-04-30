using System;
using Echo.Core.Client;

namespace Echo.Core.User
{
    public sealed class SubscriptionApprovedEventArgs : EventArgs
    {
        public IXmppClient? Connection { get; }
    }
}