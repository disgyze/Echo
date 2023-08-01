using System.Xml.Linq;

namespace Echo.Xmpp.Parser
{
    public interface IXmppElementManager : IXmppElementFactory
    {
        IXmppElementManager Register<TElement>(XName name) where TElement : XElement;
        IXmppElementManager Unregister(XName name);
    }
}