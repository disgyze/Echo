using System;

namespace Echo.Core.Client
{
    public sealed class ClientDisconnectedEventArgs : EventArgs
    {
        public IXmppClient Connection { get; }
        public string Error { get; }
        public int ErrorCode { get; }

        public ClientDisconnectedEventArgs(IXmppClient connection, string error, int errorCode)
        {
            Connection = connection;
            Error = error;
            ErrorCode = errorCode;
        }
    }
}