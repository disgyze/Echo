using System;

namespace Echo.Core.Connections
{
    public interface IXmppClientManager
    {
        int GetCount();
        IXmppClient? GetActiveClient();
        IXmppClient? GetClient(int clientIndex);
        IXmppClient? GetClient(Guid clientId);
        IXmppClient? GetClient(XmppUri accountAddress);
    }
}