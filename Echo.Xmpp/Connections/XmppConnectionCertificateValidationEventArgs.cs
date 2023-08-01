using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace Echo.Xmpp.Connections
{
    public readonly struct XmppConnectionCertificateValidationEventArgs
    {
        public X509Certificate? Certificate { get; }
        public X509Chain? Chain { get; }
        public SslPolicyErrors SslPolicyErrors { get; }

        public XmppConnectionCertificateValidationEventArgs(X509Certificate? certificate, X509Chain? chain, SslPolicyErrors sslPolicyErrors)
        {
            Certificate = certificate;
            Chain = chain;
            SslPolicyErrors = sslPolicyErrors;
        }
    }
}