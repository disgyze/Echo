using System;

namespace Echo.Core.Connections
{
    public sealed class ConnectionFailedEventArgs : EventArgs
    {
        public IXmppClient Connection { get; }
        public string Error { get; }
        public int ErrorCode { get; }

        public ConnectionFailedEventArgs(IXmppClient connection, string error, int errorCode)
        {
            Connection = connection;
            Error = error;
            ErrorCode = errorCode;
        }
    }
}