namespace Echo.Core.Messaging
{
    public readonly struct ParticipantNickChangedEventArgs
    {
        public Channel Channel { get; }
        public Participant Participant { get; }
        public string OldNick { get; }

        public ParticipantNickChangedEventArgs(Channel channel, Participant participant, string oldNick)
        {
            Channel = channel;
            Participant = participant;
            OldNick = oldNick;
        }
    }
}