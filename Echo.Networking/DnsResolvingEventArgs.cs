using System;

namespace Echo.Networking
{
    public sealed class DnsResolvingEventArgs : EventArgs
    {
        public string Host { get; }

        public DnsResolvingEventArgs(string host)
        {
            Host = host;
        }
    }
}