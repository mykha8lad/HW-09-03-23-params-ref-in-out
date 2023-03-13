using System.Text.RegularExpressions;

namespace HW_09_03_23_params_ref_in_out
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Group gr1 = new Group("DFSD", "C#", 13, 20);
            Console.WriteLine(gr1);
        }
    }
}