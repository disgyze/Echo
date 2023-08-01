namespace Echo.Core.Messaging
{
    public abstract record MucChannelJoinResult
    {
        public sealed record Success(MucChannel Channel) : MucChannelJoinResult;
        public sealed record Banned() : MucChannelJoinResult;
        public sealed record Locked() : MucChannelJoinResult;
        public sealed record NickTaken() : MucChannelJoinResult;
        public sealed record NickNotSpecified() : MucChannelJoinResult;
        public sealed record RegisteredNickRequired() : MucChannelJoinResult;
        public sealed record PasswordRequired() : MucChannelJoinResult;
        public sealed record MembershipRequired() : MucChannelJoinResult;
        public sealed record ParticipantLimitReached() : MucChannelJoinResult;
        public sealed record NotConnected() : MucChannelJoinResult;
    }
}