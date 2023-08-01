namespace Echo.Xmpp.Connections
{
    public readonly struct XmppConnectionCertificateValidationFailedEventArgs
    {
        public string ErrorMessage { get; }

        public XmppConnectionCertificateValidationFailedEventArgs(string errorMessage)
        {
            ErrorMessage = errorMessage;
        }
    }
}