namespace ShroomDB
{
    public record ShroomVersion(ushort Major, ushort Minor, ushort Patch)
    {
        public byte[] ToBytes()
        {
            byte[] bytes = new byte[6];
            Array.Copy(BitConverter.GetBytes(Major), 0, bytes, 0, 2);
            Array.Copy(BitConverter.GetBytes(Minor), 0, bytes, 2, 2);
            Array.Copy(BitConverter.GetBytes(Patch), 0, bytes, 4, 2);
            return bytes;
        }
        public static ShroomVersion FromTuple((ushort Major,ushort Minor,ushort Patch) version)
        {
            return new ShroomVersion(version.Major, version.Minor, version.Patch);
        }
        public static ShroomVersion FromBytes(byte[] bytes)
        {
            ushort major = BitConverter.ToUInt16(bytes, 0);
            ushort minor = BitConverter.ToUInt16(bytes, 2);
            ushort patch = BitConverter.ToUInt16(bytes, 4);
            return new ShroomVersion(major, minor, patch);
        }
    }
    public class Shroom : IDisposable
    {
        private ShroomBuilder builder;
        public const string Magic = "SHROOMDB";
        public int PagesAmount { get; private set; }
        public Shroom(ShroomOptions options, string filename)
        {
            builder = new ShroomBuilder(options, filename);
        }

        public void Dispose()
        {
            builder.Dispose();
        }
    }
    public record ShroomOptions(
        int PageSize,ShroomVersion Version
        )
    {
        public static readonly ShroomVersion LastVersion = new(0, 0, 0);
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
            Array.Copy(BitConverter.GetBytes(PageSize), 0, bytes, 12, 4);
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
            int pageSize = BitConverter.ToInt32(bytes, 12);
            return new ShroomOptions(pageSize, version);
        }
        public static int GetHeaderSize()
        {
            return 8 + 6 + 4;
        }
    }
    public class Token
    {
        public byte[] ID;
        public int Size;
        public object FirstLocation;
        /// <summary>
        /// over how many pages does it spread
        /// </summary>
        public int PagesSpread;
        public int TokenIndexSize => 4 + ID.Length + 4;
        public Token(byte[] id)
        {
            ID = id;
        }
    }
    internal class Page
    {
        internal int FreeSpace;

    }

}
