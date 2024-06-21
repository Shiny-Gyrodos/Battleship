using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            MainMenu.Start();
            Grid gridController = new();

            gridController.Create(gridController.playerGrid);
            gridController.Create(gridController.opponentGrid); // Makes all the grids

            gridController.Display();
            Console.Write("\nTest : Press any key to randomly place ships.");
            Console.ReadKey();

            Ship.PlaceAll(gridController.playerGrid, true);
            Ship.PlaceAll(gridController.opponentGrid, false);

            gridController.Display();

            Console.ReadKey();
        }
    }
} 