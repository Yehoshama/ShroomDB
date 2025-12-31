namespace ShroomDB
{
    public record ShroomOptions(
        int PageSize,ShroomVersion Version
        )
    {
        public static readonly ShroomVersion LastVersion = new(0, 0, 1);
        public static ShroomOptions GetOptions(int pageSize,(ushort Major,ushort Minor,ushort Patch) version)
        {
            return new ShroomOptions(pageSize, ShroomVersion.FromTuple(version));
        }
        public byte[] ToBytes()
        {
            byte[] magic = AsciiCodec.ToBytes(Shroom.Magic);
            byte[] version = Version.ToBytes();
            byte[] bytes = new byte[GetHeaderSize()];
            Array.Copy(magic, 0, bytes, 0, magic.Length);
            Array.Copy(version, 0, bytes, 8, version.Length);
            Array.Copy(BitConverter.GetBytes(PageSize), 0, bytes, 14, 4);
            return bytes;
        }
        public static ShroomOptions FromBytes( byte[] bytes )
        {
            string magic = AsciiCodec.FromBytes(bytes.Take(8).ToArray());
            if(magic != Shroom.Magic)
            {
                throw new Exception("corrupted shroom");
            }
            ShroomVersion version = ShroomVersion.FromBytes(bytes.Skip(8).Take(6).ToArray());
            int pageSize = BitConverter.ToInt32(bytes, 14);
            return new ShroomOptions(pageSize, version);
        }
        public static int GetHeaderSize()
        {
            return 8 + 6 + 4;
        }
    }

}
