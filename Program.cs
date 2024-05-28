using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            Grid gridController = new();

            gridController.Create(gridController.playerGrid);
            gridController.Create(gridController.opponentGrid); // Makes all the grids
            gridController.Create(gridController.opponentGridHidden);

            gridController.Display();
            Console.Write("\nTest : Press any key to randomly place ships.");
            Console.ReadKey();

            Ship.PlaceAll(gridController.playerGrid);
            Ship.PlaceAll(gridController.opponentGrid);

            gridController.Display();

            Console.ReadKey();
        }
    }
} 