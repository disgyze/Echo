using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel.Attention
{
	public sealed class XmppAttention : XElement
	{
		public static readonly XName ElementName = XName.Get("attention", ExtensionNamespaces.Attention);

		public XmppAttention() : base(ElementName)
		{
		}
	}
}