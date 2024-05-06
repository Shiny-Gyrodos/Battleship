using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[,] stringArray = new string[2, 3]; 
            
            Console.WriteLine(stringArray.Length);
            Console.ReadKey();
        }
    }
}