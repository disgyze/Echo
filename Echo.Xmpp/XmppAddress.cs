using System;
using System.Text;

namespace Echo.Xmpp
{
    public sealed class XmppAddress : IEquatable<XmppAddress>
    {
        public static readonly string SchemeName = "xmpp";

        public string Name { get; }
        public string Host { get; }
        public string Resource { get; }
        public string Query { get; }

        public XmppAddress(string host) : this(string.Empty, host)
        {
        }

        public XmppAddress(string? name, string host, string? resource = "", string? query = "")
        {
            if (host == null)
            {
                throw new ArgumentNullException(nameof(host));
            }

            if (host == string.Empty)
            {
                throw new ArgumentException("Cannot be empty", nameof(host));
            }

            Host = host;
            Name = name ?? string.Empty;
            Resource = resource ?? string.Empty;
            Query = query ?? string.Empty;
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
            string? query = null;

            //if (atIndex > 0)
            //         {
            //	if (schemeOffset > 0)
            //             {
            //		name = s.Substring(schemeOffset - 1, atIndex - schemeOffset + 1);
            //             }
            //             else
            //             {
            //		name = s.Substring(0, atIndex);
            //             }
            //         }

            //if (slashIndex > 0)
            //         {
            //	if (atIndex > 0)
            //             {
            //		server = s.Substring(atIndex + 1, slashIndex - atIndex - 1);
            //             }
            //             else
            //             {
            //		server = s.Substring(0, s.Length - atIndex - 1);
            //             }
            //         }

            name = atIndex > 0 ? s.Substring(Math.Max(schemeOffset - 1, 0), atIndex - (schemeOffset > 0 ? schemeOffset - 1 : 0)) : null;
            host = slashIndex > 0 ? s.Substring(atIndex + 1, slashIndex - atIndex - 1) : s.Substring(atIndex + 1, s.Length - atIndex - 1);
            resource = slashIndex > 0 ? s.Substring(slashIndex + 1, (exclamationMarkIndex > 0 ? exclamationMarkIndex - slashIndex : s.Length - slashIndex) - 1) : null;
            query = exclamationMarkIndex > 0 ? s.Substring(exclamationMarkIndex + 1, s.Length - exclamationMarkIndex - 1) : null;

            if (host == null)
            {
                throw new ArgumentException("Invalud URI", nameof(s));
            }

            return new XmppAddress(name, host, resource, query);
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
            return HashCode.Combine(Name, Host, Resource, Query);
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

            if (!string.IsNullOrEmpty(Query))
            {
                result.Append('?');
                result.Append(Query);
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