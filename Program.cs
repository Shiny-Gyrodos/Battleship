using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            MainMenu.Start();
            Grid grids = new();

            grids.Create(grids.playerGrid);
            grids.Create(grids.opponentGrid); // Makes all the grids

            grids.Display();
            Console.Write("\nTest : Press any key to randomly place ships.");
            Console.ReadKey();

            Ship.PlaceAll(grids.playerGrid, true);
            Ship.PlaceAll(grids.opponentGrid, false);

            grids.Display();

            Console.ReadKey();
        }
    }
} 