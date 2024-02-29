using System;

namespace HelloWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This is how you write to the console!");
            string cool = CheckForStuff();
            Console.WriteLine(cool);
            Console.WriteLine("NIce! It worked. Click any key to close :)");
            Console.ReadKey();
        }

        static string CheckForStuff()
        {
            string cooltxt = Console.ReadLine();
            while (string.IsNullOrEmpty(cooltxt))
            {
                cooltxt = Console.ReadLine();
            }
            return cooltxt;
        }
    }
}