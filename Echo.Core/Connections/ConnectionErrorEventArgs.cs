using System;

namespace Echo.Core.Connections
{
    public sealed class ConnectionErrorEventArgs : EventArgs
    {
        public IXmppConnection Connection { get; }
        public string Error { get; }
        public int ErrorCode { get; }

        public ConnectionErrorEventArgs(IXmppConnection connection, string error, int errorCode)
        {
            Connection = connection;
            Error = error;
            ErrorCode = errorCode;
        }
    }
}