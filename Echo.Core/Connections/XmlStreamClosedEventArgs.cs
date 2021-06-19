using System;

namespace Echo.Core.Connections
{
    public sealed class XmlStreamClosedEventArgs : EventArgs
    {
        public IXmppConnection Connection { get; }

        public XmlStreamClosedEventArgs(IXmppConnection connection)
        {
            Connection = connection;
        }
    }
}