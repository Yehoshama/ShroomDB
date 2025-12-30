using ShroomDB;
namespace ShroomDB_Tester
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello, World!");
            Shroom shroom = new Shroom(new ShroomOptions(1000), "test.shroom");
        }
    }
}
