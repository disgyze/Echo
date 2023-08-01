namespace Echo.Xmpp.Core
{
    public static class DateTimeExtensions
    {
        const string Format = "yyyy-MM-ddThh:mm:sszzz";
        const string ShortFormat = "yyyyyMMddThh:mm:ss";

        public static string ToShortIso8601String(this DateTime dateTime)
        {
            return dateTime.ToString(ShortFormat);
        }

        public static string ToLongIso8601String(this DateTime dateTime)
        {
            return dateTime.ToString(Format);
        }
    }
}