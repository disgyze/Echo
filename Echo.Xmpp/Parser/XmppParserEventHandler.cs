namespace Echo.Xmpp.Parser
{
    public delegate void XmppParserEventHandler(object sender);
    public delegate void XmppParserEventHandler<T>(object sender, T e) where T : struct;
}