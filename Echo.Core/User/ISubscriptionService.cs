using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.User
{
    public interface ISubscriptionService
    {
        Task<bool> AcceptAsync(XmppAddress address, CancellationToken cancellationToken = default);
        Task<bool> DeclineAsync(XmppAddress address, CancellationToken cancellationToken = default);
        Task<bool> SubscribeAsync(XmppAddress address, CancellationToken cancellationToken = default);      
    }
}