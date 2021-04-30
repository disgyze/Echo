using System;
using System.Collections.Generic;
using System.Text;

namespace Echo.Xmpp
{
    public struct XmppUri
    {
        public XmppAddress Address { get; }
        public IReadOnlyDictionary<string, string>? Query { get; }

        public XmppUri(XmppAddress address, IReadOnlyDictionary<string, string>? query = null)
        {
            Address = address;
            Query = query;
        }

        public static XmppUri Create(string s)
        {
            int exclamationMarkIndex = s.IndexOf('?');

            if (exclamationMarkIndex > 0)
            {

            }
        }

        public static bool TryCreate(string s, out XmppUri uri)
        {
            uri = default;
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

        public override string ToString()
        {
            return Query != null && Query.Count > 0 ? $"{Address}/{GetFormattedQuery()}" : Address;
        }

        private string GetFormattedQuery()
        {
            if (Query != null && Query.Count > 0)
            {
                var result = new StringBuilder();

                foreach (var pair in Query)
                {
                    result.Append($"{pair.Key}={Uri.EscapeUriString(pair.Value)}&");
                }

                return result.Remove(result.Length - 1, 1).ToString(); // Remove the last "&" character and return a query string
            }
            return string.Empty;
        }
    }
}