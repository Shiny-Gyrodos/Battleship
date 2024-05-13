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

            Ship.Place(gridController.playerGrid, 1);
            Ship.Place(gridController.playerGrid, 4);
            
            gridController.Display();
            Console.ReadKey();
        }
    }
} 