using System;

namespace Echo.Core.Client
{
    public interface IXmppClientManager
    {
        int Count { get; }
        IXmppClient ActiveClient { get; }

        IXmppClient GetClient(int clientIndex);
        IXmppClient GetClient(Guid clientId);
    }
}