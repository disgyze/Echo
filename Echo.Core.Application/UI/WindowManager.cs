using System;
using Echo.Core.Connections;
using Echo.Core.Messaging;

namespace Echo.Core.UI
{
    public abstract class WindowManager : IWindowManager
    {
        public abstract int Count { get; }
        public abstract Window ActiveWindow { get; }

        public abstract XmppConnection GetConnection(IWindow window);
        public abstract Conversation GetConversation(IWindow window);
        public abstract Window GetWindow(int windowIndex);
        public abstract Window GetWindow(Guid windowId);
        public abstract Window GetWindow(IConversation conversation);
        public abstract Window GetWindow(IXmppConnection connection);

        IWindow IWindowManager.ActiveWindow => ActiveWindow;
        IConversation IWindowManager.GetConversation(IWindow window) => GetConversation(window);
        IXmppConnection IWindowManager.GetConnection(IWindow window) => GetConnection(window);
        IWindow IWindowManager.GetWindow(int windowIndex) => GetWindow(windowIndex);
        IWindow IWindowManager.GetWindow(Guid windowId) => GetWindow(windowId);
        IWindow IWindowManager.GetWindow(IConversation conversation) => GetWindow(conversation);
        IWindow IWindowManager.GetWindow(IXmppConnection connection) => GetWindow(connection);
    }
}