namespace ShroomDB
{
    public record ShroomOptions(
        int PageSize, ShroomVersion Version, uint? TokenIndexOffset = null, uint? PagesIndexOffset = null
        )
    {
        public static readonly ShroomVersion LastVersion = new(0, 0, 1);
        public int PageLengthBytes { get; init; } = PageSize < 256 ? 1 : PageSize < 256 * 256 ? 2 : 4;//first version
        public static ShroomOptions GetOptions(int pageSize, (ushort Major, ushort Minor, ushort Patch) version)
        {
            return new ShroomOptions(pageSize, ShroomVersion.FromTuple(version));
        }
        public byte[] ToBytes()
        {
            byte[] magic = AsciiCodec.ToBytes(Shroom.Magic);
            byte[] version = Version.ToBytes();
            byte[] tokensOffset = TokenIndexOffset != null ? BitConverter.GetBytes(TokenIndexOffset.Value) : BitConverter.GetBytes((uint)18);
            byte[] pagesOffset = PagesIndexOffset != null ? BitConverter.GetBytes(PagesIndexOffset.Value) : BitConverter.GetBytes((uint)22);
            byte[] bytes = new byte[GetHeaderSize()];
            Array.Copy(magic, 0, bytes, 0, magic.Length);
            Array.Copy(version, 0, bytes, 8, version.Length);
            Array.Copy(BitConverter.GetBytes(PageSize), 0, bytes, 14, 4);
            Array.Copy(tokensOffset, 0, bytes, 18, 4);
            Array.Copy(pagesOffset, 0, bytes, 22, 4);
            return bytes;
        }
        public static ShroomOptions FromBytes(byte[] bytes)
        {
            string magic = AsciiCodec.FromBytes(bytes.Take(8).ToArray());
            if (magic != Shroom.Magic)
            {
                throw new Exception("corrupted shroom");
            }
            ShroomVersion version = ShroomVersion.FromBytes(bytes.Skip(8).Take(6).ToArray());
            int pageSize = BitConverter.ToInt32(bytes, 14);
            uint tokensOfsset = BitConverter.ToUInt32(bytes, 18);
            uint pagesOffset = BitConverter.ToUInt32(bytes, 22);
            return new ShroomOptions(pageSize, version, tokensOfsset, pagesOffset);
        }
        public static int GetHeaderSize()
        {
            return 8 + 6 + 4 + 4 + 4;
        }
    }

}
