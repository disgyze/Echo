using System;
using System.IO;

namespace Echo.Core.User
{
    public abstract record AvatarSource
    {
        public sealed record ByteStream(Stream Stream);
        public sealed record Url(Uri Uri);
    }
}