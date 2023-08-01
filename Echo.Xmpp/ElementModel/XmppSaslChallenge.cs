using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
    public class XmppSaslChallenge : XElement
	{
		public static readonly XName ElementName = XName.Get("challenge", XmppCoreNamespace.Sasl);

		public XmppSaslChallenge() : base(ElementName)
		{
		}

		public XmppSaslChallenge(object content) : base(ElementName, content)
		{
		}

		public XmppSaslChallenge(params object[] content) : base(ElementName, content)
		{
		}
	}
}