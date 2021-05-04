using System;

namespace Echo.Core.Messaging
{
    public struct ChatMessage : IEquatable<ChatMessage>
    {
        public string Id { get; }
        public string? Text { get; }

        public ChatMessage(string id, string? text)
        {
            Id = id;
            Text = text;
        }

        public bool Equals(ChatMessage other)
        {
            return string.Equals(this.Id, other.Id, StringComparison.OrdinalIgnoreCase);
        }

        public override bool Equals(object? obj)
        {
            return obj is ChatMessage message && Equals(message);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }

        public static bool operator ==(ChatMessage left, ChatMessage right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(ChatMessage left, ChatMessage right)
        {
            return !(left == right);
        }
    }
}