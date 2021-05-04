using System;
using System.Threading;
using System.Threading.Tasks;
using Echo.Core.UI;
using Echo.Core.User;

namespace Echo.Core.Messaging
{
    public abstract class Conversation
    {
        public Guid Id { get; }
        public Uri Address { get; }
        public IAccount Account { get; }
        public IWindow Window { get; }

        public abstract ValueTask<bool> SendActionAsync(string text, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> SendMessageAsync(string text, CancellationToken cancellationToken = default);
        public abstract ValueTask CommandAsync(string text, CancellationToken cancellationToken = default);
    }
}