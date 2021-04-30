using System;
using System.Threading;
using System.Threading.Tasks;
using Echo.Core.Client;
using Echo.Core.UI;
using Echo.Core.User;

namespace Echo.Core.Messaging
{
    public interface IConversation
    {
        Guid Id { get; }
        IWindow Window { get; }
        IAccount Account { get; }
        IXmppClient Client { get; }
       
        Task<bool> SendActionAsync(string message, CancellationToken cancellationToken = default);
        Task<bool> SendMessageAsync(string message, CancellationToken cancellationToken = default);
    }
}