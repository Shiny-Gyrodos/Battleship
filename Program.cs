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

            Console.ReadKey();
        }
    }
} 