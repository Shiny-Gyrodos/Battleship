using System;
// Idea: Store all game data in one file that you can create new instances of. All other files/classes/methods will just alter these existing variables.
//       This will make it a lot easier to create a new instance of a game if you choose to play again.

namespace MyApp
{
    internal class Program
    {
        static void Main(string[] args)
        { 
            Grid grids = new();
            grids.opponentGrid.FillWithDefaultNodes();
            grids.playerGrid.FillWithDefaultNodes();
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