using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.User
{
    public interface ISubscriptionService
    {
        Task<bool> AcceptAsync(XmppUri address, CancellationToken cancellationToken = default);
        Task<bool> DeclineAsync(XmppUri address, CancellationToken cancellationToken = default);
        Task<bool> SubscribeAsync(XmppUri address, CancellationToken cancellationToken = default);      
    }
}