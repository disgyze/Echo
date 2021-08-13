using System.Xml.Linq;

namespace Echo.Xmpp.Parser
{
    public interface IXmppElementFactory
    {
        XElement Create(XName name);
        IXmppElementFactory Register<TElement>(XName name) where TElement : XElement;
        IXmppElementFactory Unregister<TElement>() where TElement : XElement;
    }
}