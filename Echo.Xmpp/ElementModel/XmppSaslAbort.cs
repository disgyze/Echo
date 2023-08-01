using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
    public class XmppSaslAbort : XElement
	{
		public static readonly XName ElementName = XName.Get("abort", XmppCoreNamespace.Sasl);

		public XmppSaslAbort() : base(ElementName)
		{
		}
	}
}