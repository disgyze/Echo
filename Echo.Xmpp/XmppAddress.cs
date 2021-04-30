using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;

namespace Echo.Xmpp
{
	public struct XmppAddress : IEquatable<XmppAddress>
	{
		Dictionary<string, string> innerQuery;

		public static readonly XmppAddress Empty;

		public string Name { get; }
		public string Server { get; }	
		public string Resource { get; }
		public IReadOnlyDictionary<string, string> Query => innerQuery;

		public XmppAddress(string server)
		{
			if (string.IsNullOrWhiteSpace(server))
			{
				throw new ArgumentNullException(nameof(server));
			}
			Server = server;
			Name = string.Empty;
			Resource = string.Empty;
			innerQuery = new Dictionary<string, string>();
		}

		public XmppAddress(string name, string server, string resource = "", IReadOnlyDictionary<string, string> query = null)
		{
            if (string.IsNullOrWhiteSpace(server))
            {
                throw new ArgumentNullException(nameof(server));
            }

            Server = server;
            Name = name ?? string.Empty;
            Resource = resource ?? string.Empty;
            innerQuery = query != null ? new Dictionary<string, string>(query.ToDictionary(x => x.Key, y => y.Value)) : new Dictionary<string, string>();
        }

		public static XmppAddress Parse(string s)
		{
			if (!Uri.IsWellFormedUriString(s, UriKind.RelativeOrAbsolute))
			{
				throw new ArgumentException(nameof(s));
			}

			// TODO Обработать случай, когда адрес содержит схему (xmpp://, xmpp:)

			int atIndex = s.IndexOf('@');
			int slashIndex = s.IndexOf('/');
			int exclamationMarkIndex = s.IndexOf('?');

			string name = s.Substring(0, atIndex);
			string server = slashIndex > 0 ? s.Substring(atIndex + 1, slashIndex - atIndex - 1) : s.Substring(atIndex + 1, s.Length - atIndex - 1);
			string? resource = slashIndex > 0 ? s.Substring(slashIndex + 1, (exclamationMarkIndex > 0 ? exclamationMarkIndex - slashIndex : s.Length - slashIndex) - 1) : null;
			string? query = exclamationMarkIndex > 0 ? s.Substring(exclamationMarkIndex + 1, s.Length - exclamationMarkIndex - 1) : null;

			return new XmppAddress(name, server, resource, ParseQuery(query));
		}

		public static bool TryParse(string s, out XmppAddress address)
		{
			address = Empty;
			try
			{
				address = Parse(s);
				return true;
			}
			catch
			{
				return false;
			}
		}

		private static IReadOnlyDictionary<string, string> ParseQuery(string s)
		{
			if (string.IsNullOrEmpty(s))
            {
				return null;
            }

			var query = s.Split('&');
			var map = new Dictionary<string, string>();

			foreach (var pair in query)
			{
				string[] parts = pair.Split('=');

				if (parts.Length > 1)
				{
					map.Add(parts[0], parts[1]);
				}
				else
				{
					map.Add(parts[0], string.Empty);
				}
			}

			return map;
		}

		public override bool Equals(object obj)
		{
			if (!(obj is XmppAddress))
			{
				return false;
			}
			return this == (XmppAddress)obj;
		}

		public bool Equals(XmppAddress other)
		{
			return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(Server, other.Server, StringComparison.OrdinalIgnoreCase) && string.Equals(Resource, other.Resource, StringComparison.OrdinalIgnoreCase);
		}

		public bool EqualsBase(XmppAddress other)
		{
			return string.Equals(Name, other.Name, StringComparison.OrdinalIgnoreCase) && string.Equals(Server, other.Server, StringComparison.OrdinalIgnoreCase);
        }

		public override string ToString()
		{
			return GetFormattedAddress();
		}

		public string ToString(bool includeResource = true)
		{
			return GetFormattedAddress(includeResource);
		}

		public XmppAddress ToBare()
        {
			return new XmppAddress(Name, Server);
        }

		public XmppAddress ToServer()
        {
			return new XmppAddress(Server);
        }

		public override int GetHashCode()
		{
			unchecked
			{
				int hash = 17;

				if (!string.IsNullOrWhiteSpace(Name))
				{
					hash += 23 + Name.GetHashCode();
				}

				if (!string.IsNullOrWhiteSpace(Server))
				{
					hash += 23 + Server.GetHashCode();
				}

				if (!string.IsNullOrWhiteSpace(Resource))
				{
					hash += 23 + Resource.GetHashCode();
				}

				return hash;
			}
		}

		private string GetFormattedAddress(bool includeResource = true)
		{
			var result = new StringBuilder();

			result.Append(!string.IsNullOrWhiteSpace(Name) ? Name : null);
			result.Append($"@{Server}");
			result.Append(!string.IsNullOrWhiteSpace(Resource) && includeResource ? $"/{Resource}" : null);
			result.Append(Query != null && Query.Count() > 0 ? $"?{GetFormattedQuery()}" : null);

			return result.ToString();
		}

		private string GetFormattedQuery()
		{
			var sb = new StringBuilder();

			foreach (var pair in Query)
			{
				sb.Append($"{pair.Key}={Uri.EscapeUriString(pair.Value)}&");
			}

			if (sb.Length > 0)
			{
				sb.Remove(sb.Length - 1, 1);
			}

			return sb.ToString();
		}
		
		public static bool operator ==(XmppAddress left, XmppAddress right)
		{
			return left.Equals(right);
		}

		public static bool operator !=(XmppAddress left, XmppAddress right)
		{
			return !(left == right);
		}

		public static implicit operator string(XmppAddress address)
		{
			return address.ToString();
		}

		public static implicit operator ReadOnlySpan<char>(XmppAddress address)
        {
			return address.ToString().AsSpan();
        }

		public static implicit operator XmppAddress(string s)
		{
			return Parse(s);
		}
	}
}