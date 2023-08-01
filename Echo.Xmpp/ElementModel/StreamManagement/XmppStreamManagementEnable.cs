using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel.StreamManagement
{
    public sealed class XmppStreamManagementEnable : XElement
    {
        public static readonly XName ElementName = XName.Get("enable", XmppExtensionNamespace.StreamManagement);

        public XmppStreamManagementEnable() : base(ElementName)
        {
        }
    }
}