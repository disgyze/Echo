using System;

namespace Echo.Core
{
    public sealed class XmppUri : Uri
    {
        public static readonly string UriSchemeXmpp = "xmpp";

        public string? Resource => GetComponents(UriComponents.Path, UriFormat.Unescaped);

        XmppUri(string s) : base(s, UriKind.Absolute)
        {
        }

        public override string ToString()
        {
            return GetComponents(UriComponents.StrongAuthority | UriComponents.PathAndQuery, UriFormat.Unescaped);
        }

        public string ToString(bool includeResource)
        {
            return includeResource ? ToString() : GetComponents(UriComponents.StrongAuthority, UriFormat.Unescaped);
        }

        public static XmppUri Create(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentException(nameof(s));
            }

            int schemeDelimiterPos = s.IndexOf(SchemeDelimiter);

            if (schemeDelimiterPos > 0)
            {
                string scheme = s.Substring(0, schemeDelimiterPos);

                if (string.Equals(scheme, UriSchemeXmpp, StringComparison.OrdinalIgnoreCase))
                {
                    return new XmppUri(s);
                }
                else
                {
                    throw new ArgumentException("Invalid scheme");
                }
            }

            return new XmppUri(UriSchemeXmpp + SchemeDelimiter + s);
        }

        public static bool TryCreate(string s, out XmppUri uri)
        {
            uri = default!;
            try
            {
                uri = Create(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static implicit operator string(XmppUri uri) => uri.ToString();
        public static implicit operator XmppUri(string s) => XmppUri.Create(s);
    }
}