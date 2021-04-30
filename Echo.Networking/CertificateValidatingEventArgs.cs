using System;
using System.Security.Cryptography.X509Certificates;

namespace Echo.Networking
{
    public sealed class CertificateValidatingEventArgs : EventArgs
    {
        public X509Certificate2 Certificate { get; }
        public bool Accept { get; set; }

        public CertificateValidatingEventArgs(X509Certificate2 certificate, bool accept = false)
        {
            Certificate = certificate;
        }
    }
}