namespace Echo.Core.Connections
{
    public readonly struct XmppConnectionServiceErrorEventArgs
    {
        public XmppConnectionService Connection { get; }
        public string Error { get; }
        public int ErrorCode { get; }

        public XmppConnectionServiceErrorEventArgs(XmppConnectionService connection, string error, int errorCode)
        {
            Connection = connection;
            Error = error;
            ErrorCode = errorCode;
        }
    }
}