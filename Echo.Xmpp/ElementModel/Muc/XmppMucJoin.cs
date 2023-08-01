using System.Xml.Linq;

namespace Echo.Xmpp.ElementModel.Muc
{
    public class XmppMucJoin : XElement
    {
        public static readonly XName ElementName = XName.Get("x", XmppMucNamespace.Base);

        public string? Password
        {
            get => (string?)Element(ElementName.Namespace + "password");
            set => SetElementValue(ElementName.Namespace + "password", value);
        }

        public XmppMucJoin(string? password = null) : base(ElementName)
        {
            Password = password;
        }
    }
}