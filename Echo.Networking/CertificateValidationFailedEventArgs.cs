using System;

namespace Echo.Networking
{
    public sealed class CertificateValidationFailedEventArgs : EventArgs
    {
        public Exception Error { get; }

        public CertificateValidationFailedEventArgs(Exception error)
        {
            Error = error;
        }
    }
}