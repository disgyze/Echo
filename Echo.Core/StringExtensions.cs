using System;

namespace Echo.Core
{
    public static class StringExtensions
    {
        public static int ParamCount(this string s)
        {
            return s.AsSpan().ParamCount();
        }

        public static string ParamAt(this string s, int index)
        {
            return s.AsSpan().ParamAt(index).ToString();
        }

        public static string ParamOnwardAt(this string s, int index)
        {
            return s.AsSpan().ParamOnwardAt(index).ToString();
        }
    }
}