using System.IO;

namespace Echo.Xmpp.Parser
{
    internal sealed class ReusableTextReader : TextReader
    {
        string s = string.Empty;
        int pos;
        int length;

        public static ReusableTextReader Shared = new ReusableTextReader();

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing)
            {
                s = string.Empty;
                pos = 0;
                length = 0;
            }
        }

        public override int Read(char[] buffer, int index, int count)
        {
            int n = length - pos;

            if (n > 0)
            {
                if (n > count)
                {
                    n = count;
                }

                s.CopyTo(pos, buffer, index, n);
                pos += n;
            }

            return n;
        }

        public TextReader WithString(string s)
        {
            this.s = s;
            pos = 0;
            length = s.Length;
            return this;
        }
    }
}