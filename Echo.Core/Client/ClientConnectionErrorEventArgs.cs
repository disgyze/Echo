using System;

namespace Echo.Core.Client
{
    public sealed class ClientConnectionErrorEventArgs : EventArgs
    {
        public IXmppClient Connection { get; }
        public string Error { get; }
        public int ErrorCode { get; }

        public ClientConnectionErrorEventArgs(IXmppClient connection, string error, int errorCode)
        {
            Connection = connection;
            Error = error;
            ErrorCode = errorCode;
        }
    }
}