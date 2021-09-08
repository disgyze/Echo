using System;
using System.Threading.Tasks;
using Echo.Core.Connections;
using Echo.Core.UI;

namespace Echo.Core.Messaging
{
    public abstract class Conversation : IConversation
    {
        public abstract Guid Id { get; }
        public abstract Window Window { get; }
        public abstract XmppConnection Connection { get; }

        public abstract ValueTask CommandAsync(string text);
        public abstract Task<bool> SendActionAsync(string text);
        public abstract Task<bool> SendMessageAsync(string text);

        IWindow IConversation.Window => Window;
        IXmppConnection IConversation.Connection => Connection;
    }
}