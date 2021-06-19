using System;

namespace Echo.Core
{
    public sealed class XmppUri : Uri
    {
        string? fullXmppString = null;
        string? bareXmppString = null;
        string? resource = null;

        public static readonly string UriSchemeXmpp = "xmpp";

        public string? Resource
        {
            get
            {
                if (resource == null)
                {
                    resource = GetComponents(UriComponents.Path, UriFormat.Unescaped);
                }
                return resource;
            }
        }

        XmppUri(string s) : base(s, UriKind.Absolute)
        {
        }

        public string ToUriString()
        {
            return base.ToString();
        }

        public override string ToString()
        {
            if (fullXmppString == null)
            {
                fullXmppString = GetComponents(UriComponents.StrongAuthority | UriComponents.PathAndQuery, UriFormat.Unescaped);
            }
            return fullXmppString;
        }

        public string ToBareString()
        {
            if (bareXmppString == null)
            {
                bareXmppString = GetComponents(UriComponents.StrongAuthority, UriFormat.Unescaped);
            }
            return bareXmppString;
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
                var scheme = s.AsSpan().Slice(0, schemeDelimiterPos);

                if (scheme.Equals(UriSchemeXmpp, StringComparison.OrdinalIgnoreCase))
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

        public static bool TryCreate(string s, out XmppUri? uri)
        {
            try
            {
                uri = Create(s);
                return true;
            }
            catch
            {
                uri = default;
                return false;
            }
        }

        public static bool Equals(XmppUri left, XmppUri right, XmppUriComparison comparison = XmppUriComparison.Full)
        {
            if (object.ReferenceEquals(left, right))
            {
                return true;
            }

            if (left == null || right == null)
            {
                return false;
            }

            if (comparison == XmppUriComparison.Full)
            {
                return left == right;
            }
            else
            {
                return string.Equals(
                    left.GetComponents(UriComponents.StrongAuthority, UriFormat.Unescaped), 
                    right.GetComponents(UriComponents.StrongAuthority, UriFormat.Unescaped), 
                    StringComparison.OrdinalIgnoreCase);
            }
        }

        public static implicit operator string(XmppUri uri) => uri.ToString();
        public static implicit operator XmppUri(string s) => XmppUri.Create(s);
    }
}