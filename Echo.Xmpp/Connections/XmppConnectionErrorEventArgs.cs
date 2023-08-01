namespace Echo.Xmpp.Connections
{
    public readonly struct XmppConnectionErrorEventArgs
    {
        public string ErrorMessage { get; }
        public int ErrorCode { get; }

        public XmppConnectionErrorEventArgs(string errorMessage, int errorCode)
        {
            ErrorMessage = errorMessage;
            ErrorCode = errorCode;
        }
    }
}