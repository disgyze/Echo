using System;
using Echo.Core.Connections;

namespace Echo.Core.Messaging
{
    public abstract class Conversation
    {
        public XmppAddress Address { get; }
        public XmppConnectionService Connection { get; }

        protected Conversation(XmppAddress address, XmppConnectionService connection)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
            Connection = connection ?? throw new ArgumentNullException(nameof(connection));
        }
    }
}