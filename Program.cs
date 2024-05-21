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
            Console.ReadKey();

            Ship.Place(gridController.playerGrid, 1); // Issues with placing multiple ships. Occasional error for placing individual ships aswell.
            gridController.Display();
            Console.ReadKey();
            Ship.Place(gridController.playerGrid, 4);
            gridController.Display();
            Console.ReadKey();
            Ship.Place(gridController.playerGrid, 2);
            gridController.Display();
            Console.ReadKey();
            Ship.Place(gridController.playerGrid, 5);
            gridController.Display();
            Console.ReadKey();
            Ship.Place(gridController.playerGrid, 3);
            
            gridController.Display();
            Console.ReadKey();
        }
    }
} 