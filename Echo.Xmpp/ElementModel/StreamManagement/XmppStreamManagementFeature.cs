using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel.StreamManagement
{
    public sealed class XmppStreamManagementFeature : XElement
    {
        public static readonly XName ElementName = XName.Get("sm", ExtensionNamespaces.StreamManagement);

        public XmppStreamManagementFeature() : base(ElementName)
        {
        }
    }
}