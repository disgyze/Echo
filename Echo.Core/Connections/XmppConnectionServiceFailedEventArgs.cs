namespace Echo.Core.Connections
{
    public readonly struct XmppConnectionServiceFailedEventArgs
    {
        public XmppConnectionService Connection { get; }
        public string ErrorMessage { get; }
        public int ErrorCode { get; }

        public XmppConnectionServiceFailedEventArgs(XmppConnectionService connection, string error, int errorCode)
        {
            Connection = connection;
            ErrorMessage = error;
            ErrorCode = errorCode;
        }
    }
}