using ShroomDB;
namespace ShroomDB_Tester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var verstion = ShroomOptions.LastVersion;
            Shroom shroom = new Shroom(new ShroomOptions(1000, verstion), "test.shroom");
        }
    }
}
