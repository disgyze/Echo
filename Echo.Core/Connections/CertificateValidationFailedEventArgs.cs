namespace Echo.Core.Connections
{
    public readonly struct CertificateValidationFailedEventArgs
    {
        public XmppConnectionService Connection { get; }
        public string ErrorMessage { get; }

        public CertificateValidationFailedEventArgs(XmppConnectionService connection, string errorMessage)
        {
            Connection = connection;
            ErrorMessage = errorMessage;
        }
    }
}