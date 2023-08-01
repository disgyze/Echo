using System.Xml.Linq;

namespace Echo.Xmpp.Parser
{
    public interface IXmppElementFactory
    {
        XElement Create(XName name);
    }
}