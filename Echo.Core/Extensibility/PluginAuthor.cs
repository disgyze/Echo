using System;

namespace Echo.Core.Extensibility
{
    public sealed class PluginAuthor
    {
        public string Name { get; }
        public Uri? Email { get; }
        public Uri? XmppAddress { get; }

        public PluginAuthor(string name, Uri? email, Uri? address)
        {
            Name = name;
            Email = email;
            XmppAddress = address;
        }
    }
}