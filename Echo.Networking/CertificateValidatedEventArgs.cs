using System;
using System.Security.Cryptography.X509Certificates;

namespace Echo.Networking
{
    public sealed class CertificateValidatedEventArgs : EventArgs
    {
        public X509Certificate2 Certificate { get; }

        public CertificateValidatedEventArgs(X509Certificate2 certificate)
        {
            Certificate = certificate;
        }
    }
}