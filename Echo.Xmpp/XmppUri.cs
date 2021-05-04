using System;

namespace Echo.Xmpp
{
    public static class XmppUri
    {
       public static Uri Create(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException(nameof(s));
            }

            if (s.StartsWith("xmpp://") || s.StartsWith("xmpp:"))
            {
                return new Uri(s);
            }

            return new Uri($"xmpp://{s}");
        }
    }
}