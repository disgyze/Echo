using System;

namespace Echo.Core.Connections
{
    public sealed class XmlStreamClosedEventArgs : EventArgs
    {
        public IXmppClient Connection { get; }

        public XmlStreamClosedEventArgs(IXmppClient connection)
        {
            Connection = connection;
        }
    }
}