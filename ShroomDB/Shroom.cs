namespace ShroomDB
{
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

}
