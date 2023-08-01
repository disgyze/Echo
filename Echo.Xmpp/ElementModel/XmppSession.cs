using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
	public class XmppSession : XElement
	{
		public static readonly XName ElementName = XName.Get("session", XmppCoreNamespace.Session);

		public XmppSession() : base(ElementName)
		{
		}
	}
}