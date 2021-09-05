using System;
using Echo.Core.Connections;
using Echo.Core.Messaging;

namespace Echo.Core.UI
{
    public interface IWindowManager
    {
        int Count { get; }
        IWindow? ActiveWindow { get; }

        IWindow? GetWindow(int windowIndex);
        IWindow? GetWindow(Guid windowId);
        IWindow? GetWindow(IConversation conversation);
        IWindow? GetWindow(IXmppConnection connection);
        IConversation? GetConversation(IWindow window);
        IXmppConnection? GetConnection(IWindow window);
    }
}