using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
    public class XmppSaslResponse : XElement
	{
		public static readonly XName ElementName = XName.Get("response", XmppCoreNamespace.Sasl);

		public XmppSaslResponse(object content) : base(ElementName, content)
		{
		}
	}
}