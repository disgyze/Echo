namespace Echo.Core.Connections
{
    public readonly struct CertificateValidationSucceedEventArgs
    {
        public XmppConnectionService Connection { get; }

        public CertificateValidationSucceedEventArgs(XmppConnectionService connection)
        {
            Connection = connection;
        }
    }
}