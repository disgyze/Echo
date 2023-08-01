using System;

namespace Echo.Networking
{
    internal static class ByteArrayExtensions
    {
        public static byte[] Append(this byte[] source, byte[] buffer)
        {
            var result = new byte[source.Length + buffer.Length];

            Buffer.BlockCopy(source, 0, result, 0, source.Length);
            Buffer.BlockCopy(buffer, 0, result, source.Length, buffer.Length);

            return result;
        }
    }
}