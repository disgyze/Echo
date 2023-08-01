namespace Echo.Core.Connections
{
    public readonly struct XmlStreamOpenedEventArgs
    {
        public string Host { get; }
        public XmppConnectionService Connection { get; }

        public XmlStreamOpenedEventArgs(string host, XmppConnectionService connection)
        {
            Host = host;
            Connection = connection;
        }
    }
}