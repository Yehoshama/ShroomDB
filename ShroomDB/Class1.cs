namespace ShroomDB
{
    
    internal static class ChainManager
    {

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
