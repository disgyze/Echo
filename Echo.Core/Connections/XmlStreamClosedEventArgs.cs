namespace Echo.Core.Connections
{
    public readonly struct XmlStreamClosedEventArgs
    {
        public XmppConnectionService Connection { get; }

        public XmlStreamClosedEventArgs(XmppConnectionService connection)
        {
            Connection = connection;
        }
    }
}