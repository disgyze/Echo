using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel.StreamManagement
{
    public sealed class XmppStreamManagementFeature : XElement
    {
        public static readonly XName ElementName = XName.Get("sm", XmppExtensionNamespace.StreamManagement);

        public XmppStreamManagementFeature() : base(ElementName)
        {
        }
    }
}