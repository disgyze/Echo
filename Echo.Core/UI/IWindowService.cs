using System;
using Echo.Core.Connections;
using Echo.Core.Messaging;

namespace Echo.Core.UI
{
    public interface IWindowService
    {
        int Count { get; }
        IWindow? ActiveWindow { get; }

        IWindow? GetWindow(int windowIndex);
        IWindow? GetWindow(Guid windowId);
        IChannel? GetChannel(IWindow window);
        IDirectChat? GetDirectChat(IWindow window);
        IPrivateChat? GetPrivateChat(IWindow window);
        IXmppConnection? GetConnection(IWindow window);
    }
}