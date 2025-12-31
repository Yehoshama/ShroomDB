namespace ShroomDB
{
    internal enum FlagSemantics
    {
        IsNegativeOffset = 0,
        IsSegmentEnd = 1,
        IsContinueOnPage = 2,
        IsContinueCrossPage = 3
    }
    internal static class ChainManager
    {
        public record SegmentMetadata(ushort CurrentPosition, ushort Length, ushort NextPosition, byte Flags, int? NextPage, ushort? NextPagePosition);
        internal static bool AddSegmentMeta(Stream stream, int position, byte[] data, ushort nextOffset, byte flags)
        {
            stream.Position = position;
            stream.Write(BitConverter.GetBytes((ushort)data.Length));
            stream.Write(data,0, data.Length);
            stream.Write(BitConverter.GetBytes(nextOffset));
            stream.Write([flags]);
            return true;
        }
        internal static byte FlagsToByte(bool isNegativeOffset,bool isSegmentEnd, bool isContinueOnPage, bool isContinueCrossPage)
        {
            byte b = 0;
            if (isNegativeOffset) Utility.BitFlags.SetBit(ref b, (int)FlagSemantics.IsNegativeOffset);
            if (isSegmentEnd) Utility.BitFlags.SetBit(ref b, (int)FlagSemantics.IsSegmentEnd);
            if (isContinueOnPage) Utility.BitFlags.SetBit(ref b, (int)FlagSemantics.IsContinueOnPage);
            if (isContinueCrossPage) Utility.BitFlags.SetBit(ref b, (int)FlagSemantics.IsContinueCrossPage);
            return b;
        }
        internal static (bool isNegativeOffset,bool isSegmentEnd, bool isContinueOnPage, bool isContinueCrossPage) ByteToFlags(byte @byte)
        {
            bool isNegativeOffset = Utility.BitFlags.GetBit(@byte, (int)FlagSemantics.IsNegativeOffset);
            bool isSegmentEnd = Utility.BitFlags.GetBit(@byte, (int)FlagSemantics.IsSegmentEnd);
            bool isContinueOnPage = Utility.BitFlags.GetBit(@byte, (int)FlagSemantics.IsContinueOnPage);
            bool isContinueCrossPage = Utility.BitFlags.GetBit(@byte, (int)FlagSemantics.IsContinueCrossPage);
            return (isNegativeOffset, isSegmentEnd, isContinueOnPage, isContinueCrossPage);
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
