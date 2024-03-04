using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;

namespace HelloWorld
{

    class Cat
    {
        public string name;
        public int age;

        public void meow()
        {
            Console.WriteLine("Meow name is " + name + ", meownd I am " + age + " years old.");
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            CultureInfo culture = new CultureInfo("en-US");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;
            Console.Title = "IAAB";
            Console.ForegroundColor = ConsoleColor.Green;


            Console.WriteLine("This is how you write to the console!");

            Cat cat1 = new Cat();
            Cat cat2 = new Cat();

            cat1.name = "Heiso";
            cat2.name = "Josef";

            cat1.age = 2;
            cat2.age = 7;

            cat1.meow();
            cat2.meow();



            /*
            Console.Write("Write a number you want to be squared: ");
            int squarable =  Convert.ToInt16(Console.ReadLine());
            Console.WriteLine("If you square your number " + squarable + ", the result will be: " + squared(squarable));
            
            meetAlien();

            Console.WriteLine("Write something, and then it will respond!");
            string cool = CheckForStuff();
            Console.WriteLine(cool);

            Console.WriteLine("NIce! It worked. Now Lets try the calculator :)");
            Calculator();
            
            Console.WriteLine("Wwawww, now check out my IQ test...");
            Conditions();

            Console.WriteLine("Look at these loops:");
            Loops();

            Console.WriteLine("Look at these arrays I made:");
            ArraysAndLists();*/

            Console.WriteLine("Ok! Click any key to exit :>");
            Console.ReadKey();
        }

        static int squared(int a)
        {
            int result = a * a;
            return result;
        }

        
        static void meetAlien()
        {
            Random numGen = new Random();
            string name = "x-" + numGen.Next(100,99999);
            int age = numGen.Next(50, 1000);
            Console.WriteLine("Hello! My name is " + name + ", and I am " + age + " years old.");
            Console.WriteLine("Oh yeah, I'm an Alien! :>");
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

        static void Calculator()
        {
            double first;
            double second;
            Console.Write("Enter the first number: ");
            first = Convert.ToDouble(Console.ReadLine());
            Console.Write("Enter the second number: ");
            second = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine(first + " * " + second + " = " + (first*second));
        }

        static void Conditions()
        {
            Console.WriteLine("Welcome, what is my name? ^^");
            string myName = Console.ReadLine();
            if (myName == "IAAB")
            {
                Console.WriteLine("Correct!");
            }
            else
            {
                Console.WriteLine("No, my name is IAAB");
            }
        }

        static void Loops()
        {
            Console.WriteLine("This is a 'for' loop that will print 10 numbers after eachother:");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("This is a 'while' loop that will print out 10 numbers but they are random...");
            int num = 10;
            Random ranNum = new Random();
            while (num > 0)
            {
                int numba = ranNum.Next(0, 100);
                Console.WriteLine(numba);
                num--;
            }
        }

        static void ArraysAndLists()
        {
            int[] intArray = new int[3];
            intArray[0] = 1;
            intArray[1] = 2;
            intArray[2] = 3;
            string[] strArray = { "Arrays", "are", "finite" };
            foreach (var item in strArray)
            {
                Console.WriteLine(item);
            }

            for (int i = 0; i < intArray.Length; i++)
            {
                Console.WriteLine(intArray[i]);
            }

            object[] mixedArray = { 1, true, "three" };
            int othernum = 0;
            while (othernum < mixedArray.Length)
            {
                Console.WriteLine(mixedArray[othernum]);
                othernum++;
            }



            Console.WriteLine("Lists:");
            List<string> strList = new List<string>();
            strList.Add("THis was adddedd");
            strList.Add("THis was also adddedd");
            Console.WriteLine(strList[strList.Count - 1]);
            strList.RemoveAt(0);
            strList.Remove("THis was also adddedd");
            Console.WriteLine("List has " + strList.Count + " lines left.");
        }
    }
}