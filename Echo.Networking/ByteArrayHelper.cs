using System;
using System.Linq;

namespace Echo.Networking
{
    internal static class ByteArrayHelper
    {
        public static byte[] GetNullTerminator()
        {
            return new byte[] { 0 };
        }

        public static byte[] Combine(params byte[][] buffers)
        {
            byte[] result = new byte[buffers.Sum(x => x.Length)];
            int offset = 0;

            foreach (byte[] buffer in buffers)
            {
                Buffer.BlockCopy(buffer, 0, result, offset, buffer.Length);
                offset += buffer.Length;
            }

            return result;
        }
    }
}