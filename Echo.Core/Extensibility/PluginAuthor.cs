using System;
using Echo.Xmpp;

namespace Echo.Core.Extensibility
{
    public sealed class PluginAuthor
    {
        public string Name { get; }
        public Uri? Email { get; }
        public XmppAddress? XmppAddress { get; }

        public PluginAuthor(string name, Uri email, XmppAddress address)
        {
            Name = name;
            Email = email;
            XmppAddress = address;
        }
    }
}