using System;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.User
{
    public interface ISubscriptionService
    {
        ValueTask<bool> AcceptAsync(XmppUri address, CancellationToken cancellationToken = default);
        ValueTask<bool> DeclineAsync(XmppUri address, CancellationToken cancellationToken = default);
        ValueTask<bool> SubscribeAsync(XmppUri address, CancellationToken cancellationToken = default);      
    }
}