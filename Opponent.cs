using System.Diagnostics;
using System.Linq.Expressions;

class Opponent : Attacks
{
    // Consider looking into adding a constructor class.
    // Consider adding a list named something like "queuedCoordPairs" that stores coord pairs that need the surrounding area checked.
    static List<(int, int)> coordPairs = [];
    static Random rng = new();
    static (int vertical, int horizontal) coords = (rng.Next(0, 8), rng.Next(0, 8));
    static int emptyLocationsFound = 0;
    public static void Turn()
    {
        
    }


    // This method is used when the ships's orientation has been found.
    static void FinishingOffShip(Grid grids) // Haven't yet accounted for when the first two coord pairs acquired belong to different ships.
    {
        bool validNodeFiredAt = false;

        while (!validNodeFiredAt && emptyLocationsFound < 2)
        {
            if (coordPairs[0].Item1 == coordPairs[1].Item1)
            {
                (int same, int modified) acquiredCoords = (coordPairs[0].Item1, FetchModifiedCoord(coordPairs[0].Item2, coordPairs[1].Item2));
                validNodeFiredAt = Shoot(grids.playerGrid, acquiredCoords);
                emptyLocationsFound = grids.playerGrid[acquiredCoords.same, acquiredCoords.modified].NodeFilled == true ? emptyLocationsFound : emptyLocationsFound + 1;
            }
            else
            {
                (int modified, int same) acquiredCoords = (FetchModifiedCoord(coordPairs[0].Item1, coordPairs[1].Item1), coordPairs[0].Item2);
                validNodeFiredAt = Shoot(grids.playerGrid, acquiredCoords);
                emptyLocationsFound = grids.playerGrid[acquiredCoords.modified, acquiredCoords.same].NodeFilled == true ? emptyLocationsFound : emptyLocationsFound + 1;
            }
        }
    }



    static int FetchModifiedCoord(int coord1, int coord2) // Will eventually return a number 1 greater or lower than the two numbers fed in.
    {
        int modifiedCoord = coord1;

        while (modifiedCoord == coord1 || modifiedCoord == coord2)
        {
            modifiedCoord += rng.Next(0, 2);
            modifiedCoord -= rng.Next(0, 2);
        }

        return modifiedCoord;
    }


    // This method is for when one segment of a ship has been found. 
    // It is searching for the ship's orientation.
    static bool FindShipPosition(Grid grids, (int vertical, int horizontal) coords)
    {
        (List<int> vertical, List<int> horizontal) increments = ([1, -1], [1, -1]);

        while (true) // True since there is gauranteed to be a second ship segments if this method is called.
        {
            bool firstCheckVertical = rng.Next(1, 3) > 1.5;

            if (firstCheckVertical && increments.vertical.Count > 0)
            {
                int randomIncrement = increments.vertical[rng.Next(0, increments.vertical.Count)];

                if (Shoot(grids.playerGrid, (coords.vertical + increments.vertical[randomIncrement], coords.horizontal))) 
                {     
                    return grids.playerGrid[coords.vertical, coords.horizontal].NodeFilled == true;
                }
                
                increments.vertical.Remove(randomIncrement); 
            }
            else if (!firstCheckVertical && increments.horizontal.Count > 0)
            {
                int randomIncrement = increments.horizontal[rng.Next(0, increments.horizontal.Count)];

                if (Shoot(grids.playerGrid, (coords.vertical, coords.horizontal + increments.horizontal[randomIncrement])))
                {
                    return grids.playerGrid[coords.vertical, coords.horizontal].NodeFilled == true;
                }
                
                increments.horizontal.Remove(randomIncrement); 
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
               
            (coords.vertical, coords.horizontal) = (rng.Next(0, 8), rng.Next(0, 8));
        }
    }
}