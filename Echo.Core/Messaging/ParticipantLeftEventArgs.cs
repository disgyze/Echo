namespace Echo.Core.Messaging
{
    public readonly struct ParticipantLeftEventArgs
    {
        public Channel Channel { get; }
        public Participant Participant { get; }
        public string? Reason { get; }

        public ParticipantLeftEventArgs(Channel channel, Participant participant, string? reason = null)
        {
            Channel = channel;
            Participant = participant;
            Reason = reason;
        }
    }
}