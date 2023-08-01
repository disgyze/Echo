using Echo.Core.User;

namespace Echo.Core.Messaging
{
    public sealed class MucParticipant : Participant
    {
        public MucParticipantRole Role { get; internal set; }
        public MucParticipantAffiliation Affiliation { get; internal set; }

        public MucParticipant(Channel channel, XmppAddress address, string nick, Presence presence, MucParticipantRole role, MucParticipantAffiliation affiliation) : base(channel, address, nick, presence)
        {
            Role = role;
            Affiliation = affiliation;
        }
    }
}