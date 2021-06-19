using System;
using System.Threading.Tasks;
using Echo.Core.Connections;
using Echo.Core.Extensibility;
using Echo.Core.UI;

namespace Echo.Core.Messaging
{
    public interface IConversation : ISupportsCommand
    {
        Guid Id { get; }
        IWindow Window { get; }
        IXmppConnection Connection { get; }
       
        ValueTask<bool> SendActionAsync(string text);
        ValueTask<bool> SendMessageAsync(string text);
    }
}