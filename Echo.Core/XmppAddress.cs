using System;
using System.Text;

namespace Echo.Core
{
    public sealed class XmppAddress : IEquatable<XmppAddress>
    {
        public static readonly string SchemeName = "xmpp";

        public string? Name { get; }
        public string Host { get; }
        public string? Resource { get; }

        public XmppAddress(string host)
        {
            if (string.IsNullOrWhiteSpace(host))
            {
                throw new ArgumentException(nameof(host));
            }

            Host = host;
            Name = null;
            Resource = null;
        }

        public XmppAddress(string? name, string host, string? resource = "", string? query = "") : this(host)
        {
            if (host == string.Empty)
            {
                throw new ArgumentException("Cannot be empty", nameof(host));
            }

            Host = host;
            Name = name ?? string.Empty;
            Resource = resource ?? string.Empty;
        }

        public static XmppAddress Create(string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                throw new ArgumentNullException(nameof(s));
            }

            int schemeDelimiterIndex = s.IndexOf(Uri.SchemeDelimiter);
            int schemeOffset = 0;

            if (schemeDelimiterIndex > 0)
            {
                string scheme = s.Substring(0, schemeDelimiterIndex);

                if (!scheme.Equals(SchemeName, StringComparison.OrdinalIgnoreCase))
                {
                    throw new ArgumentException("Invalid scheme name");
                }

                schemeOffset = scheme.Length + schemeDelimiterIndex;
            }

            int atIndex = s.IndexOf('@');
            int slashIndex = s.IndexOf('/', Math.Max(atIndex, 0));
            int exclamationMarkIndex = s.IndexOf('?', Math.Max(slashIndex, 0));

            string? name = null;
            string? host = null;
            string? resource = null;

            name = atIndex > 0 ? s.Substring(Math.Max(schemeOffset - 1, 0), atIndex - (schemeOffset > 0 ? schemeOffset - 1 : 0)) : null;
            host = slashIndex > 0 ? s.Substring(atIndex + 1, slashIndex - atIndex - 1) : s.Substring(atIndex + 1, s.Length - atIndex - 1);
            resource = slashIndex > 0 ? s.Substring(slashIndex + 1, (exclamationMarkIndex > 0 ? exclamationMarkIndex - slashIndex : s.Length - slashIndex) - 1) : null;

            if (host == null)
            {
                throw new ArgumentException("Invalud URI", nameof(s));
            }

            return new XmppAddress(name, host, resource);
        }

        public static bool TryCreate(string s, out XmppAddress? address)
        {
            try
            {
                address = Create(s);
                return true;
            }
            catch
            {
                address = null;
                return false;
            }
        }

        public override bool Equals(object? obj)
        {
            return obj is XmppAddress other && this.Equals(other);
        }

        public bool Equals(XmppAddress? other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(Host, other.Host, StringComparison.OrdinalIgnoreCase) && string.Equals(Resource, other.Resource, StringComparison.OrdinalIgnoreCase);
        }

        public bool EqualsBare(XmppAddress? other)
        {
            if (object.ReferenceEquals(other, null))
            {
                return false;
            }

            if (object.ReferenceEquals(this, other))
            {
                return true;
            }

            return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(Host, other.Host, StringComparison.OrdinalIgnoreCase);
        }

        public override string ToString()
        {
            return GetFormattedAddress();
        }

        public string ToBareString()
        {
            return GetFormattedAddress(includeResource: false);
        }

        public XmppAddress ToBare()
        {
            return new XmppAddress(Name, Host);
        }

        public XmppAddress ToServer()
        {
            return new XmppAddress(Host);
        }

        public Uri ToUri()
        {
            return new Uri($"{SchemeName}{Uri.SchemeDelimiter}{ToString()}", UriKind.Absolute);
        }

        public Uri ToBareUri()
        {
            return new Uri($"{SchemeName}{Uri.SchemeDelimiter}{ToBareString()}", UriKind.Absolute);
        }

        public Uri ToServerUri()
        {
            return new Uri($"{SchemeName}{Uri.SchemeDelimiter}{ToServer()}", UriKind.Absolute);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Host, Resource);
        }

        private string GetFormattedAddress(bool includeResource = true)
        {
            var result = new StringBuilder();

            if (!string.IsNullOrEmpty(Name))
            {
                result.Append(Name);
                result.Append('@');
            }

            result.Append(Host);

            if (includeResource && !string.IsNullOrEmpty(Resource))
            {
                result.Append('/');
                result.Append(Resource);
            }

            return result.ToString();
        }

        public static bool operator ==(XmppAddress left, XmppAddress right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(XmppAddress left, XmppAddress right)
        {
            return !(left == right);
        }

        public static implicit operator XmppAddress(string value) => XmppAddress.Create(value);
        public static implicit operator string(XmppAddress value) => value.ToString();
    }
}