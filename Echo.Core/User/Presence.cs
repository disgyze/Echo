namespace Echo.Core.User
{
    public sealed record Presence(PresenceKind Kind, string? Text = null)
    {
        public static Presence Offline(string? text = null) => new Presence(PresenceKind.Away, text);
        public static Presence Chat(string? text = null) => new Presence(PresenceKind.Chat, text);
        public static Presence Away(string? text = null) => new Presence(PresenceKind.Away, text);
        public static Presence ExtendedAway(string? text = null) => new Presence(PresenceKind.ExtendedAway, text);
        public static Presence DoNotDisturb(string? text = null) => new Presence(PresenceKind.DoNotDisturb, text);
    }
}