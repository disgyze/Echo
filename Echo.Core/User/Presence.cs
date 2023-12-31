﻿using System;

namespace Echo.Core.User
{
    public readonly struct Presence : IEquatable<Presence>
    {
        public string? Text { get; }
        public PresenceState State { get; }

        public Presence(string? text, PresenceState state)
        {
            Text = text;
            State = state;
        }

        public override bool Equals(object? obj)
        {
            return obj is Presence presence && Equals(presence);
        }

        public bool Equals(Presence other)
        {
            return string.Equals(Text, other.Text, StringComparison.OrdinalIgnoreCase) && State == other.State;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Text, State);
        }

        public static bool operator ==(Presence left, Presence right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(Presence left, Presence right)
        {
            return !(left == right);
        }
    }
}