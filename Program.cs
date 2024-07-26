using System;

namespace MyApp
{
    internal class Program
    {
        static void Main()
        { 
            string playerChoice = "yes";

            while (playerChoice == "yes")
            {
                Game.Start();

                Console.Clear();
                Console.WriteLine("Would you like to play again? YES/NO");
                playerChoice = Console.ReadLine().ToLower() ?? "no";
            }
        }
    }
} 