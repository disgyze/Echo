using System;

namespace Echo.Core.Extensibility
{
    public sealed class PluginAuthor
    {
        public string Name { get; }
        public Uri? EmailAddress { get; }
        public XmppAddress? XmppAddress { get; }

        public PluginAuthor(string name, Uri? emailAddress, XmppAddress? xmppAddress)
        {
            Name = name;
            EmailAddress = emailAddress;
            XmppAddress = xmppAddress;
        }
    }
}