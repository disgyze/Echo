using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.Messaging
{
    public abstract class ParticipantStore
    {
        public abstract ValueTask<bool> SaveParticipantAsync(Participant participant, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> UpdateParticipantAsync(Participant participant, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> DeleteParticipantAsync(Participant participant, CancellationToken cancellationToken = default);
        public abstract ValueTask<bool> ClearAsync(CancellationToken cancellationToken = default);
        public abstract ValueTask<Participant?> GetParticipantAsync(XmppAddress address, CancellationToken cancellationToken = default);
        public abstract ValueTask<Participant?> GetParticipantAsync(string nick, CancellationToken cancellationToken = default);
        public abstract IAsyncEnumerable<Participant> GetParticipantsAsync();
    }
}