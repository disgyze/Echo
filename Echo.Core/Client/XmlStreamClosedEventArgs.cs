using System;

namespace Echo.Core.Client
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