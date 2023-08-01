using System;

namespace Echo.Core
{
    public static class ReadOnlySpanOfCharExtensions
    {
        public static int IndexOf(this ReadOnlySpan<char> span, char c, int startIndex, StringComparison comparisonType = StringComparison.OrdinalIgnoreCase)
        {
            var sliceIndex = span.Slice(startIndex).IndexOf(new ReadOnlySpan<char>(c), comparisonType);
            return sliceIndex == -1 ? -1 : startIndex + sliceIndex;
        }

        static int GetFirstNonWhitespaceCharPos(ref ReadOnlySpan<char> s)
        {
            int count = s.Length;

            if (count == 0)
            {
                return -1;
            }

            for (int i = 0; i < count; i++)
            {
                if (!char.IsWhiteSpace(s[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        static int GetLastWhitespacePos(ref ReadOnlySpan<char> s, int startIndex)
        {
            int count = s.Length;

            if (count == 0)
            {
                return -1;
            }

            char currChar = default;
            char prevChar = default;

            for (int i = startIndex; i < count; i++)
            {
                currChar = s[i];

                if (i >= 1)
                {
                    prevChar = s[i - 1];
                }

                if (!char.IsWhiteSpace(currChar) && char.IsWhiteSpace(prevChar))
                {
                    return i;
                }
            }

            return -1;
        }

        public static int ParamCount(this ReadOnlySpan<char> s)
        {
            int i = GetFirstNonWhitespaceCharPos(ref s);

            if (i == -1)
            {
                return 0;
            }

            int paramIndex = 0;
            do
            {
                i = GetLastWhitespacePos(ref s, i + 1);
                paramIndex++;
            }
            while (i > 0);

            return paramIndex;
        }

        public static ReadOnlySpan<char> ParamAt(this ReadOnlySpan<char> s, int index)
        {
            int count = s.Length;

            if (count == 0 || index < 0 || index >= count)
            {
                return ReadOnlySpan<char>.Empty;
            }

            int startIndex = GetFirstNonWhitespaceCharPos(ref s);

            if (index == 0)
            {
                int whitespacePos = s.IndexOf(' ', startIndex);
                int endIndex = whitespacePos > 0 ? whitespacePos - startIndex : count - startIndex;

                return s.Slice(startIndex, endIndex);
            }

            int paramIndex = 0;
            char prevChar = default;
            char currChar = default;

            for (int i = startIndex; i < count; i++)
            {
                currChar = s[i];

                if (i >= 1)
                {
                    prevChar = s[i - 1];
                }

                if (char.IsWhiteSpace(currChar))
                {
                    if (!char.IsWhiteSpace(prevChar))
                    {
                        paramIndex++;
                    }
                }
                else
                {
                    if (index == paramIndex)
                    {
                        int lastWhitespacePos = s.IndexOf(' ', i + 1);
                        int endIndex = lastWhitespacePos > 0 ? lastWhitespacePos - i : count - i;

                        return s.Slice(i, endIndex);
                    }
                }
            }

            return ReadOnlySpan<char>.Empty;
        }

        public static ReadOnlySpan<char> ParamOnwardAt(this ReadOnlySpan<char> s, int index)
        {
            int count = s.Length;

            if (count == 0 || index < 0 || index >= count)
            {
                return ReadOnlySpan<char>.Empty;
            }

            if (index == 0)
            {
                return s;
            }
           
            int paramIndex = 0;
            char prevChar = default;
            char currChar = default;
            bool nonWhiteSpaceCharFound = false;

            for (int i = 0; i < count; i++)
            {
                currChar = s[i];

                if (i >= 1)
                {
                    prevChar = s[i - 1];
                }

                if (char.IsWhiteSpace(currChar))
                {
                    if (!nonWhiteSpaceCharFound)
                    {
                        continue;
                    }

                    if (!char.IsWhiteSpace(prevChar))
                    {
                        paramIndex++;
                    }
                }
                else
                {
                    nonWhiteSpaceCharFound = true;

                    if (index == paramIndex)
                    {
                        return s.Slice(i, count - i);
                    }
                }
            }

            return ReadOnlySpan<char>.Empty;
        }
    }
}