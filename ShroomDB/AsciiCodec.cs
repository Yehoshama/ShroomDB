namespace ShroomDB
{
    public static class AsciiCodec
    {
        public static byte[] ToBytes(string ascii)
        {
            byte[] bytes = new byte[ascii.Length];
            for (int i = 0; i < ascii.Length; i++)
            {
                char c = ascii[i];
                if (c > 127)
                    throw new ArgumentException("Non-ASCII character detected.");

                bytes[i] = (byte)c;
            }
            return bytes;
        }

        public static string FromBytes(byte[] bytes)
        {
            char[] chars = new char[bytes.Length];
            for (int i = 0; i < bytes.Length; i++)
            {
                if (bytes[i] > 127)
                    throw new ArgumentException("Non-ASCII byte detected.");

                chars[i] = (char)bytes[i];
            }
            return new string(chars);
        }
    }

}
