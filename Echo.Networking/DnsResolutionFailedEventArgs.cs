using System;

namespace Echo.Networking
{
    public sealed class DnsResolutionFailedEventArgs : EventArgs
    {
        public Exception Error { get; }

        public DnsResolutionFailedEventArgs(Exception error)
        {
            Error = error;
        }
    }
}