using System;
using System.Net;

namespace Echo.Networking
{
    /// <summary>
    /// Used by the <see cref="Echo.Networking.Connections.WebSocketConnectionFactory"/>
    /// </summary>
    public sealed class UriEndPoint : EndPoint
    {
        public Uri Uri { get; }

        public UriEndPoint(Uri uri)
        {
            Uri = uri ?? throw new ArgumentNullException(nameof(uri));
        }

        public override string ToString()
        {
            return Uri.ToString();
        }
    }
}