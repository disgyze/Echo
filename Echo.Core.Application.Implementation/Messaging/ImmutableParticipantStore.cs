using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Echo.Core.Messaging
{
    public sealed class ImmutableParticipantStore : ParticipantStore
    {
        ImmutableList<Participant> participantCollection;

        public ImmutableParticipantStore(ImmutableList<Participant>? participantCollection = null)
        {
            this.participantCollection = participantCollection ?? ImmutableList<Participant>.Empty;
        }

        public override ValueTask<bool> SaveParticipantAsync(Participant participant, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (participant is null)
            {
                throw new ArgumentNullException(nameof(participant));
            }

            static ImmutableList<Participant> Save(ImmutableList<Participant> collection, Participant participant) =>
                collection.FirstOrDefault(otherParticipant => otherParticipant.Address == participant.Address) is null ? collection.Add(participant) : collection;

            return ValueTask.FromResult(ImmutableInterlocked.Update(ref participantCollection, Save, participant));
        }

        public override ValueTask<bool> UpdateParticipantAsync(Participant participant, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (participant is null)
            {
                throw new ArgumentNullException(nameof(participant));
            }

            static ImmutableList<Participant> Update(ImmutableList<Participant> collection, Participant participant) =>
                collection.FirstOrDefault(otherParticipant => otherParticipant.Address == participant.Address) is Participant otherParticipant ? collection.Replace(otherParticipant, participant) : collection;

            return ValueTask.FromResult(ImmutableInterlocked.Update(ref participantCollection, Update, participant));
        }

        public override ValueTask<bool> DeleteParticipantAsync(Participant participant, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (participant is null)
            {
                throw new ArgumentNullException(nameof(participant));
            }

            static ImmutableList<Participant> Delete(ImmutableList<Participant> collection, Participant participant) =>
                collection.FirstOrDefault(otherParticipant => otherParticipant.Address == participant.Address) is Participant otherParticipant ? collection.Remove(participant) : collection;

            return ValueTask.FromResult(ImmutableInterlocked.Update(ref participantCollection, Delete, participant));
        }

        public override ValueTask<bool> ClearAsync(CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            return ValueTask.FromResult(ImmutableInterlocked.Update(ref participantCollection, collection => collection.Clear()));
        }

        public override ValueTask<Participant?> GetParticipantAsync(XmppAddress address, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (address is null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            return ValueTask.FromResult(participantCollection.FirstOrDefault(participant => participant.Address == address));
        }

        public override ValueTask<Participant?> GetParticipantAsync(string nick, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(nick))
            {
                throw new ArgumentException(null, nameof(nick));
            }

            return ValueTask.FromResult(participantCollection.FirstOrDefault(participant => string.Equals(participant.Nick, nick, StringComparison.OrdinalIgnoreCase)));
        }

        public override async IAsyncEnumerable<Participant> GetParticipantsAsync()
        {
            foreach (var participant in participantCollection)
            {
                yield return participant;
            }
            await Task.CompletedTask.ConfigureAwait(false);
        }
    }
}