namespace Echo.Xmpp.ElementModel
{
	public static class XmppCoreNamespace
	{
		public static readonly string FeatureAuth = "http://jabber.org/features/iq-auth";
		public static readonly string Bind = "urn:ietf:params:xml:ns:xmpp-bind";
		public static readonly string Client = "jabber:client";
		public static readonly string Compress = "http://jabber.org/protocol/compress";	
        public static readonly string FeatureCompression = "http://jabber.org/features/compress";	
		public static readonly string Roster = "jabber:iq:roster";	
		public static readonly string Sasl = "urn:ietf:params:xml:ns:xmpp-sasl";
		public static readonly string ServiceDiscoveryInfo = "http://jabber.org/protocol/disco#info";
		public static readonly string ServiceDiscoveryItems = "http://jabber.org/protocol/disco#items";
		public static readonly string Session = "urn:ietf:params:xml:ns:xmpp-session";
		public static readonly string StanzaError = "urn:ietf:params:xml:ns:xmpp-stanzas";
		public static readonly string Stream = "http://etherx.jabber.org/streams";
		public static readonly string StreamError = "urn:ietf:params:xml:ns:xmpp-streams";
		public static readonly string Tls = "urn:ietf:params:xml:ns:xmpp-tls";
	}
}