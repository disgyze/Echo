using System;
using System.Security.Cryptography.X509Certificates;

namespace Echo.Core.Connections
{
    public sealed class CertificateValidatingEventArgs : EventArgs
    {
        public IXmppClient Connection { get; }
        public X509Certificate Certificate { get; }
        public bool Accept { get; set; }

        public CertificateValidatingEventArgs(IXmppClient connection, X509Certificate certificate, bool accept = true)
        {
            Connection = connection;
            Certificate = certificate;
            Accept = accept;
        }
    }
}