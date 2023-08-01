using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Echo.Xmpp.ElementModel;
using Echo.Xmpp.ElementModel.Blocklist;

namespace Echo.Xmpp.Parser
{
    public sealed class XmppElementManager : IXmppElementManager
    {
        Dictionary<XName, Type> map = new Dictionary<XName, Type>();

        public XElement Create(XName name)
        {
            if (map.TryGetValue(name, out var type))
            {
                var element = (XElement?)Activator.CreateInstance(type);

                if (element is null)
                {
                    return new XElement(name);
                }

                return element;
            }
            return new XElement(name);
        }

        public IXmppElementManager Register<TElement>(XName name) where TElement : XElement
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

        public IXmppElementManager Unregister(XName name)
        {
            map.Remove(name);
            return this;
        }

        public static IXmppElementManager CreateDefault()
        {
            return new XmppElementManager()
                .Register<XmppStreamError>(XmppStreamError.ElementName)
                .Register<XmppStreamIdentity>(XmppStreamIdentity.ElementName)
                .Register<XmppStreamFeature>(XmppStreamFeature.ElementName)
                .Register<XmppStreamFeatureCollection>(XmppStreamFeatureCollection.ElementName)
                .Register<XmppSession>(XmppSession.ElementName)
                .Register<XmppBind>(XmppBind.ElementName)
                .Register<XmppPing>(XmppPing.ElementName)
                .Register<XmppIQ>(XmppIQ.ElementName)
                .Register<XmppMessage>(XmppMessage.ElementName)
                .Register<XmppPresence>(XmppPresence.ElementName)
                .Register<XmppTlsFailure>(XmppTlsFailure.ElementName)
                .Register<XmppTlsProceed>(XmppTlsProceed.ElementName)
                .Register<XmppSaslChallenge>(XmppSaslChallenge.ElementName)
                .Register<XmppSaslResponse>(XmppSaslResponse.ElementName)
                .Register<XmppSaslSuccess>(XmppSaslSuccess.ElementName)
                .Register<XmppSaslFailure>(XmppSaslFailure.ElementName)
                .Register<XmppSaslMechanismCollection>(XmppSaslMechanismCollection.ElementName)
                .Register<XmppRoster>(XmppRoster.ElementName)
                .Register<XmppRosterItem>(XmppRosterItem.ElementName)
                .Register<XmppSoftwareVersion>(XmppSoftwareVersion.ElementName)
                .Register<XmppBlocklist>(XmppBlocklist.ElementName)
                .Register<XmppBlocklistItem>(XmppBlocklistItem.ElementName);
        }
    }
}