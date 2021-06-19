using System;

namespace Echo.Core.Connections
{
    public sealed class XmlStreamOpenEventArgs : EventArgs
    {
        public IXmppConnection Connection { get; }

        public XmlStreamOpenEventArgs(IXmppConnection connection)
        {
            Connection = connection;
        }
    }
}