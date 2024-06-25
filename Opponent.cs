using System.Diagnostics;
using System.Linq.Expressions;

class Opponent : Attacks
{
    // Consider looking into adding a struct.
    static List<(int, int)> coordPairs = [];
    static Random rng = new();
    static (int vertical, int horizontal) coords = (rng.Next(0, 8), rng.Next(0, 8));
    public static void Turn()
    {
        
    }



    static void FinishingOffShip(Grid grids)
    {
        if (coordPairs[0].Item1 == coordPairs[1].Item1)
        {

        }
        else
        {

        }
    }


    // This method is for when one segment of a ship has been found. 
    // It is searching for the ship's orientation.
    static bool FindShipPosition(Grid grids, (int vertical, int horizontal) coords)
    {
        (List<int> vertical, List<int> horizontal) = ([1, -1], [1, -1]);

        while (true) // True since there is gauranteed to be a second ship segments if this method is called.
        {
            bool firstCheckVertical = rng.Next(1, 3) > 1.5;

            if (firstCheckVertical && vertical.Count > 0)
            {
                int randomIncrement = vertical[rng.Next(0, vertical.Count)];

                if (Shoot(grids.playerGrid, (coords.vertical + vertical[randomIncrement], coords.horizontal))) 
                {     
                    return grids.playerGrid[coords.vertical, coords.horizontal].NodeFilled == true;
                }
                else
                {
                    vertical.Remove(randomIncrement); 
                }
            }
            else if (!firstCheckVertical && horizontal.Count > 0)
            {
                int randomIncrement = horizontal[rng.Next(0, horizontal.Count)];

                if (Shoot(grids.playerGrid, (coords.vertical, coords.horizontal + horizontal[randomIncrement])))
                {
                    return grids.playerGrid[coords.vertical, coords.horizontal].NodeFilled == true;
                }
                else
                {
                    vertical.Remove(randomIncrement); 
                }
            }
        }
    }



    // Searching for ships by randomly firing.
    static bool HuntForShipSegments(Grid grids, (int vertical, int horizontal) coords) // Searches randomly for ships.
    {
        while (true)
        {
            if (Shoot(grids.playerGrid, coords))
            {

                return grids.playerGrid[coords.vertical, coords.horizontal].NodeFilled == true;
            }
            else
            {
                (coords.vertical, coords.horizontal) = (rng.Next(0, 8), rng.Next(0, 8));
            }
        }
    }
}