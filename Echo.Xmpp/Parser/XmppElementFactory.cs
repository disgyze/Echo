using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace Echo.Xmpp.Parser
{
    public sealed class XmppElementFactory : IXmppElementFactory
    {
        Dictionary<XName, Type> map = new Dictionary<XName, Type>();

        public XElement Create(XName name)
        {
            if (map.ContainsKey(name))
            {
                return (XElement)Activator.CreateInstance(map[name])!;
            }
            return new XElement(name);
        }

        public IXmppElementFactory Register<TElement>(XName name) where TElement : XElement
        {
            Type type = typeof(TElement);

            if (map.ContainsKey(name))
            {
                map[name] = type;
            }
            else
            {
                map.Add(name, type);
            }

            return this;
        }

        public IXmppElementFactory Unregister<TElement>() where TElement : XElement
        {
            Type type = typeof(TElement);

            if (map.ContainsValue(type))
            {
                map.Remove(map.FirstOrDefault(p => p.Value == type).Key);
            }

            return this;
        }
    }
}