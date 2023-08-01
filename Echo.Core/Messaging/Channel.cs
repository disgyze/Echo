using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading;
using System.Threading.Tasks;
using Echo.Core.Connections;

namespace Echo.Core.Messaging
{
    public abstract class Channel : Conversation
    {
        ImmutableList<Participant> participantCollection = ImmutableList<Participant>.Empty;

        public bool IsJoined { get; internal set; }
        public IReadOnlyList<Participant> Participants => participantCollection;

        protected private Channel(XmppAddress address, XmppConnectionService connection) : base(address, connection)
        {
        }

        internal void Add(Participant participant)
        {
            ImmutableInterlocked.Update(ref participantCollection, static (collection, participant) =>
            {
                return collection.Find(other => other.Address == participant.Address) switch
                {
                    Participant oldParticipant => collection.Replace(oldParticipant, participant),
                    null => collection.Add(participant)
                };
            }, participant);
        }

        internal void Remove(Participant participant)
        {
            ImmutableInterlocked.Update(ref participantCollection, static (collection, participant) =>
            {
                return collection.Find(other => other.Address == participant.Address) switch
                {
                    Participant oldParticipant => collection.Remove(oldParticipant),
                    null => collection
                };
            }, participant);
        }
    }
}