using System;

namespace Echo.Core.Connections
{
    public sealed class XmlStreamOpenEventArgs : EventArgs
    {
        public IXmppClient Connection { get; }

        public XmlStreamOpenEventArgs(IXmppClient connection)
        {
            Connection = connection;
        }
    }
}