using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
    public class XmppTlsFailure : XElement
	{
		public static readonly XName ElementName = XName.Get("failure", XmppCoreNamespace.Tls);

		public XmppTlsFailure() : base(ElementName)
		{
		}
	}
}