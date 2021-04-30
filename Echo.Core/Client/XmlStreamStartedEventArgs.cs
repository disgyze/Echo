using System;

namespace Echo.Core.Client
{
    public sealed class XmlStreamStartedEventArgs : EventArgs
    {
        public IXmppClient Connection { get; }

        public XmlStreamStartedEventArgs(IXmppClient connection)
        {
            Connection = connection;
        }
    }
}