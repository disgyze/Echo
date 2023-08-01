using Echo.Core.User;

namespace Echo.Core.Messaging
{
    public readonly struct ParticipantStatusChangedEventArgs
    {
        public Channel Channel { get; }
        public Participant Participant { get; }
        public Presence OldStatus { get; }

        public ParticipantStatusChangedEventArgs(Channel channel, Participant participant, Presence oldStatus)
        {
            Channel = channel;
            Participant = participant;
            OldStatus = oldStatus;
        }
    }
}