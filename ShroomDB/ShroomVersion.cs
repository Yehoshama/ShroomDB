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

}
