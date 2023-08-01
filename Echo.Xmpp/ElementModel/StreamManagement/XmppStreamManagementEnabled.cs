using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel.StreamManagement
{
    public sealed class XmppStreamManagementEnabled : XElement
    {
        public static readonly XName ElementName = XName.Get("enabled", XmppExtensionNamespace.StreamManagement);

        public string? Id
        {
            get => (string)Attribute("id")!;
            set => SetAttributeValue("id", value);
        }

        public string? Location
        {
            get => (string)Attribute("location")!;
            set => SetAttributeValue("location", Value);
        }

        public bool? Resume
        {
            get => (bool?)Attribute("resume");
            set => SetAttributeValue("resume", value);
        }

        public XmppStreamManagementEnabled() : base(ElementName)
        {
        }
    }
}