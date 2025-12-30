using System.Threading.Tasks.Dataflow;

namespace ShroomDB
{
    internal class ShroomBuilder : IDisposable
    {
        internal ShroomOptions Options;
        internal string FileName;
        internal FileStream? Stream;
        internal int PagesAmount;
        private readonly byte[] ZeroPage;
        internal ShroomBuilder( ShroomOptions options, string fileName )
        {
            Options = options;
            FileName = fileName;
            ZeroPage = new byte[Options.PageSize];

            if (File.Exists(FileName))
            {
                //read header...
                Stream = new FileStream(
                    fileName,
                    FileMode.Open,
                    FileAccess.ReadWrite,
                    FileShare.None
                );
                Stream.Position = 0;
                byte[] header = new byte[ShroomOptions.GetHeaderSize()];
                Stream.Read(header, 0, header.Length);
                Options = ShroomOptions.FromBytes(header);
            }
            else
            {
                //create file...
                Stream = new FileStream(
                    fileName,
                    FileMode.CreateNew,
                    FileAccess.ReadWrite,
                    FileShare.None
                );
                Stream.Position = 0;
                var header = Options.ToBytes();
                Stream.Write(header, 0, header.Length);
                Stream.Write(ZeroPage, 0, Options.PageSize);
                Stream.Flush();
            }
        }
        public Page AddPage()
        {
            Stream.Seek(0, SeekOrigin.End);
            Stream.Write(ZeroPage, 0, Options.PageSize);
            Stream.Flush();
            return new Page();//placeholder
        }
        public bool RemovePage(Page page)
        {//placeholder
            return true;
        }
        public bool RemovePage(int pageNumber)
        {//placeholder
            return true;
        }
        public void Dispose()
        {
            Stream?.Dispose();
        }
    }
}
