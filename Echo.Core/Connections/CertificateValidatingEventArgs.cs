using System.Security.Cryptography.X509Certificates;

namespace Echo.Core.Connections
{
    public readonly struct CertificateValidatingEventArgs
    {
        public XmppConnectionService Connection { get; }
        public X509Certificate? Certificate { get; }

        public CertificateValidatingEventArgs(XmppConnectionService connection, X509Certificate? certificate)
        {
            Connection = connection;
            Certificate = certificate;
        }
    }
}