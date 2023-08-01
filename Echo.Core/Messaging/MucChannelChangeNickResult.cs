namespace Echo.Core.Messaging
{
    public abstract record MucChannelChangeNickResult
    {
        public sealed record Success(string NewNick) : MucChannelChangeNickResult;
        public sealed record Locked() : MucChannelChangeNickResult;
        public sealed record AlreadyTaken() : MucChannelChangeNickResult;
        public sealed record NotConnected() : MucChannelChangeNickResult;
    }
}