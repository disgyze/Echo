using System;

namespace Echo.Core
{
    public sealed class XmppAddress : IEquatable<XmppAddress>
    {
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

        public XmppAddress(string? name, string host, string? resource = "") : this(host)
        {
            Host = host;
            Name = name;
            Resource = resource;
        }

        public static XmppAddress Create(string s)
        {
            int atIndex = s.IndexOf('@');
            int slashIndex = s.IndexOf('/');

            string? name = ExtractName(s, atIndex);
            string? host = ExtractHost(s, atIndex, slashIndex);
            string? resource = ExtractResource(s, slashIndex);

            if (string.IsNullOrWhiteSpace(host))
            {
                throw new XmppAddressFormatException();
            }

            return new XmppAddress(name, host, resource);
        }

        private static string? ExtractName(string s, int atIndex)
        {
            return atIndex > 0 ? s.Substring(0, atIndex) : null;
        }

        private static string? ExtractHost(string s, int atIndex, int slashIndex)
        {
            return (atIndex, slashIndex) switch
            {
                ( >= 0, > 0) => s.Substring(atIndex + 1, slashIndex - atIndex - 1),
                ( >= 0, < 0) => s.Substring(atIndex + 1, s.Length - atIndex - 1),
                ( < 0, > 0) => s.Substring(0, s.Length - slashIndex - 1),
                _ => s
            };
        }

        private static string? ExtractResource(string s, int slashIndex)
        {
            return slashIndex > 0 ? s.Substring(slashIndex + 1, s.Length - slashIndex - 1) : null;
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
            return other is not null && string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(Host, other.Host, StringComparison.OrdinalIgnoreCase) && string.Equals(Resource, other.Resource, StringComparison.OrdinalIgnoreCase);
        }

        public bool EqualsBare(XmppAddress? other)
        {
            return other is not null && string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(Host, other.Host, StringComparison.OrdinalIgnoreCase);
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
            if (string.IsNullOrEmpty(Resource) || string.IsNullOrEmpty(Name))
            {
                return this;
            }
            return new XmppAddress(Name, Host);
        }

        public XmppAddress ToServer()
        {
            return new XmppAddress(Host);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Host, Resource);
        }

        private string GetFormattedAddress(bool includeResource = true)
        {
            bool hasName = !string.IsNullOrEmpty(Name);
            bool hasResource = !string.IsNullOrEmpty(Resource);

            int length = 0;
            length += hasName? Name!.Length + 1 : 0; // Name.Length + '@'
            length += includeResource ? Resource!.Length + 1 : 0; // Resource.Length + '/'
            length += Host.Length;

            (string Name, string Host, string Resource, bool IncludeResource) state = (Name ?? string.Empty, Host, Resource ?? string.Empty, includeResource);

            return string.Create(length, state, static (span, state) =>
            {
                int offset = 0;

                if (state.Name != string.Empty)
                {
                    string name = state.Name!;
                    int nameLength = name.Length;
                    offset += nameLength + 1;

                    for (int i = 0; i < nameLength; i++)
                    {
                        span[i] = name[i];
                    }

                    span[nameLength] = '@';
                }

                int hostLength = state.Host.Length;                

                for (int i = 0; i < hostLength; i++)
                {
                    span[i + offset] = state.Host[i];
                }

                offset += hostLength;

                if (state.Resource != string.Empty && state.IncludeResource)
                {
                    span[offset] = '/';
                    offset += 1;

                    string resource = state.Resource!;
                    int resourceLength = resource.Length;

                    for (int i = 0; i < resourceLength; i++)
                    {
                        span[i + offset] = resource[i];
                    }
                }
            });
        }

        public static bool Equals(XmppAddress? left, XmppAddress? right)
        {
            return left is not null && right is not null && left.Equals(right);
        }

        public static bool EqualsBare(XmppAddress? left, XmppAddress? right)
        {
            return left is not null && right is not null && left.EqualsBare(right);
        }

        public static bool operator ==(XmppAddress? left, XmppAddress? right)
        {
            return left is not null && right is not null && left.Equals(right);
        }

        public static bool operator !=(XmppAddress? left, XmppAddress? right)
        {
            return !(left == right);
        }

        public static implicit operator XmppAddress(string value) => XmppAddress.Create(value);
        public static implicit operator string(XmppAddress value) => value.ToString();
    }
}