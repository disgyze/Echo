using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Echo.Core
{
	public static class StringExtensions
	{
		public static bool IsWildMatch(this string s, string mask, bool ignoreCase = true)
		{
			if (string.Compare(s, mask, StringComparison.OrdinalIgnoreCase) == 0)
			{
				return true;
			}

			if (!string.IsNullOrWhiteSpace(s) && !string.IsNullOrWhiteSpace(mask))
			{
				string pattern = '^' + Regex.Escape(mask).Replace(@"\*", ".*").Replace(@"\?", "?") + "$";
				return Regex.IsMatch(s, pattern, ignoreCase ? RegexOptions.IgnoreCase : RegexOptions.None);
			}

			return false;
		}

		public static bool IsNumeric(this string s)
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				return false;
			}

			foreach (char c in s)
			{
				if (c < '0' || c > '9')
				{
					return false;
				}
			}

			return true;
		}

		static int GetFirstNonWhitespaceCharPos(string s)
		{
			for (int i = 0; i < s.Length; i++)
			{
				if (!char.IsWhiteSpace(s[i]))
				{
					return i;
				}
			}
			return 0;
		}

		static int GetLastWhitespacePos(string s, int startIndex)
		{
			char currChar = char.MinValue;
			char prevChar = char.MinValue;

			for (int i = startIndex; i < s.Length; i++)
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

			return 0;
		}

		public static int ParamCount(this string s)
		{
			int i = GetFirstNonWhitespaceCharPos(s);
			int count = 0;

			do
			{
				i = GetLastWhitespacePos(s, i + 1);
				count += 1;
			}
			while (i > 0);

			return count;
		}

		public static string Param(this string s, int index)
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				return string.Empty;
			}

			if (index < 0 || index > s.Length - 1)
			{
				throw new IndexOutOfRangeException();
			}

			int startFrom = GetFirstNonWhitespaceCharPos(s);

			if (index == 0)
			{
				int whitespacePos = s.IndexOf(' ', startFrom);
				int count = whitespacePos > 0 ? whitespacePos - startFrom : s.Length - startFrom;

				return s.Substring(startFrom, count);
			}

			int whitespaceCount = 0;
			char prevChar = char.MinValue;
			char currChar = char.MinValue;

			for (int i = startFrom; i < s.Length; i++)
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
						whitespaceCount += 1;
					}
				}
				else
				{
					if (index == whitespaceCount)
					{
						int lastWhitespacePos = s.IndexOf(' ', i + 1);
						int count = lastWhitespacePos > 0 ? lastWhitespacePos - i : s.Length - i;

						return s.Substring(i, count);
					}
				}
			}

			return string.Empty;
		}

		public static string ParamOnward(this string s, int index)
		{
			if (string.IsNullOrWhiteSpace(s))
			{
				return string.Empty;
			}

			if (index < 0 || index > s.Length - 1)
			{
				throw new IndexOutOfRangeException();
			}

			if (index == 0)
			{
				// TODO
				return s;
			}

			int whitespaceCount = 0;
			char prevChar = char.MinValue;
			char currChar = char.MinValue;
			bool charFound = false;

			for (int i = 0; i < s.Length; i++)
			{
				currChar = s[i];

				if (i >= 1)
				{
					prevChar = s[i - 1];
				}

				if (char.IsWhiteSpace(currChar))
				{
					if (!charFound)
					{
						continue;
					}

					if (!char.IsWhiteSpace(prevChar))
					{
						whitespaceCount += 1;
					}
				}
				else
				{
					charFound = true;

					if (index == whitespaceCount)
					{
						return s.Substring(i, s.Length - i);
					}
				}
			}

			return string.Empty;
		}

		public static string LeftSubstring(this string s, int count)
		{
			if (count < s.Length)
			{
				return s.Substring(0, count);
			}
			return s;
		}

		public static string RightSubstring(this string s, int count)
		{
			if (count < s.Length)
			{
				return s.Substring(s.Length - count, count);
			}
			return s;
		}

		public static string Replace(this string s, IReadOnlyDictionary<string, string> map)
		{
			StringBuilder sb = new StringBuilder(s);

			foreach (var pair in map)
			{
				sb.Replace(pair.Key, pair.Value);
			}

			return sb.ToString();
		}

		public static string Replace(this string s, IReadOnlyDictionary<string, Func<string>> map)
		{
			StringBuilder sb = new StringBuilder(s);

			foreach (var pair in map)
			{
				sb.Replace(pair.Key, pair.Value());
			}

			return sb.ToString();
		}
	}
}