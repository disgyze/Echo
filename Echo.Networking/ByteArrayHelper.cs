using System.IO;

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
            using var ms = new MemoryStream();

            foreach (var buffer in buffers)
            {
                ms.Write(buffer);
            }

            return ms.ToArray();
        }
    }
}