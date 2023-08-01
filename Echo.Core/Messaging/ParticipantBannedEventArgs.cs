namespace Echo.Core.Messaging
{
    public readonly struct ParticipantBannedEventArgs
    {
        public Channel Channel { get; }
        public Participant Sender { get; }
        public Participant Target { get; }
        public string? Reason { get; }

        public ParticipantBannedEventArgs(Channel channel, Participant sender, Participant target, string? reason = null)
        {
            Channel = channel;
            Sender = sender;
            Target = target;
            Reason = reason;
        }
    }
}