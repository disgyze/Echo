using System;

namespace Echo.Core.Client
{
    public sealed class ClientConnectionFailedEventArgs : EventArgs
    {
        public IXmppClient Connection { get; }
        public string Error { get; }
        public int ErrorCode { get; }

        public ClientConnectionFailedEventArgs(IXmppClient connection, string error, int errorCode)
        {
            Connection = connection;
            Error = error;
            ErrorCode = errorCode;
        }
    }
}