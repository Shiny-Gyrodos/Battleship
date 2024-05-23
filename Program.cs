using System;

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            Display display = new();

            display.CreateGrid(display.playerGrid);
            display.CreateGrid(display.opponentGrid); // Makes all the grids
            display.CreateGrid(display.opponentGridHidden);

            Console.ReadKey();
        }
    }
} 