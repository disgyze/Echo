using System;

namespace Echo.Core.Connections
{
    public sealed class ActiveConnectionChangedEventArgs : EventArgs
    {
        public IXmppConnection? OldConnection { get; }
        public IXmppConnection? NewConnection { get; }

        public ActiveConnectionChangedEventArgs(IXmppConnection? oldConnection, IXmppConnection? newConnection)
        {
            OldConnection = oldConnection;
            NewConnection = newConnection;
        }
    }
}