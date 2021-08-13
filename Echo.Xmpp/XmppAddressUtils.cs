using System;

namespace Echo.Xmpp
{
    public static class XmppAddressUtils
    {
        //public static string Create(string server)
        //{
        //	if (string.IsNullOrWhiteSpace(server))
        //	{
        //		return string.Empty;
        //	}
        //	else
        //	{
        //		return string.Format("@{0}", server);
        //	}
        //}

        //public static string Create(string userName, string server)
        //{
        //	if (string.IsNullOrWhiteSpace(userName) && string.IsNullOrWhiteSpace(server))
        //	{
        //		return string.Empty;
        //	}
        //	else
        //	{
        //		return string.Format("{0}@{1}", userName, server);
        //	}
        //}

        //public static string Create(string userName, string server, string resource)
        //{
        //	if (string.IsNullOrWhiteSpace(userName) && string.IsNullOrWhiteSpace(server) && string.IsNullOrWhiteSpace(resource))
        //	{
        //		return string.Empty;
        //	}
        //	else
        //	{
        //		return string.Format("{0}@{1}/{2}", userName, server, resource);
        //	}
        //}

        public static string ExtractBase(string s)
        {
            int slashIndex = s.IndexOf('/');

            if (slashIndex > 0)
            {
                return s.Substring(0, s.Length - slashIndex - 1);
            }
            else
            {
                return s;
            }
        }

        public static string ExtractUserName(string s)
        {
            return s.Substring(0, s.IndexOf('@'));
        }

        public static string ExtractServer(string s)
        {
            int atIndex = s.IndexOf('@');
            int slashIndex = s.IndexOf('/');

            if (slashIndex > 0)
            {
                return s.Substring(atIndex + 1, s.Length - atIndex - 1);
            }
            else
            {
                return s.Substring(atIndex + 1, slashIndex - atIndex - 1);
            }
        }

        public static string ExtractResource(string s)
        {
            int slashIndex = s.IndexOf('/');
            return s.Substring(slashIndex + 1, s.Length - slashIndex - 1);
        }

        public static bool TryExtractBase(string s, out string result)
        {
            result = null!;
            try
            {
                result = ExtractBase(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryExtractUserName(string s, out string result)
        {
            result = string.Empty;
            try
            {
                result = ExtractUserName(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryExtractServer(string s, out string result)
        {
            result = string.Empty;
            try
            {
                result = ExtractServer(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool TryExtractResource(string s, out string result)
        {
            result = string.Empty;
            try
            {
                result = ExtractResource(s);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static int Compare(string a, string b)
        {
            if (object.ReferenceEquals(a, b))
            {
                return 0;
            }

            if (!Uri.IsWellFormedUriString(a, UriKind.Absolute) || !Uri.IsWellFormedUriString(b, UriKind.Absolute))
            {
                return -1;
            }

            return string.Compare(a, b, StringComparison.OrdinalIgnoreCase);
        }

        public static int CompareBase(string a, string b)
        {
            if (object.ReferenceEquals(a, b))
            {
                return 0;
            }

            if (!Uri.IsWellFormedUriString(a, UriKind.Absolute) || !Uri.IsWellFormedUriString(b, UriKind.Absolute))
            {
                return -1;
            }

            return string.Compare(ExtractBase(a), ExtractBase(b), StringComparison.OrdinalIgnoreCase);
        }

        public static bool Equals(string a, string b)
        {
            return Compare(a, b) == 0;
        }

        public static bool EqualsBase(string a, string b)
        {
            return CompareBase(a, b) == 0;
        }
    }
}