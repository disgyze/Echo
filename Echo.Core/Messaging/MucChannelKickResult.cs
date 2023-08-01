namespace Echo.Core.Messaging
{
    public abstract record MucChannelKickResult
    {
        public sealed record Success() : MucChannelKickResult;
        public sealed record NotConnected() : MucChannelKickResult;
    }
}