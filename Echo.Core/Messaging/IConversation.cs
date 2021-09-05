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
       
        Task<bool> SendActionAsync(string text);
        Task<bool> SendMessageAsync(string text);
    }
}