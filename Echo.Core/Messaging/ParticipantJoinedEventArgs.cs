namespace Echo.Core.Messaging
{
    public readonly struct ParticipantJoinedEventArgs
    {
        public Channel Channel { get; }
        public Participant Participant { get; }

        public ParticipantJoinedEventArgs(Channel channel, Participant participant)
        {
            Channel = channel;
            Participant = participant;
        }
    }
}