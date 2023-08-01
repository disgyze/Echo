using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel
{
	public class XmppCompressionFeature : XElement
	{
		public static readonly XName ElementName = XName.Get("compression", XmppCoreNamespace.FeatureCompression);

		public XmppCompressionFeature() : base(ElementName)
		{
		}
	}

	public class XmppCompress : XElement
	{
		public static readonly XName ElementName = XName.Get("compress", XmppCoreNamespace.Compress);

		public XmppCompress(string algorithm) : base(ElementName)
		{
			Add(new XElement(Name.Namespace + "method", algorithm));
		}
	}

	public class XmppCompressed : XElement
	{
		public static readonly XName ElementName = XName.Get("compressed", XmppCoreNamespace.Compress);

		public XmppCompressed() : base(ElementName)
		{
		}
	}
}