using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
    public class XmppSaslMechanismCollection : XElement
	{
		public static readonly XName ElementName = XName.Get("mechanisms", XmppCoreNamespace.Sasl);

		public XmppSaslMechanismCollection() : base(ElementName)
		{
		}
	}
}