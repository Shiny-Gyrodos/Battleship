using System.Diagnostics;
using System.Linq.Expressions;

class Opponent : Attacks
{
    // Consider looking into adding a constructor class.
    // Consider adding a list named something like "queuedCoordPairs" that stores coord pairs that need the surrounding area checked.
    static List<(int, int)> coordPairs = [];
    static List<(int, int)>[] coordSearchQueue = [[], []]; // The first list is for positively modified coords while the second is for negatively.
    static Random rng = new();
    static (int vertical, int horizontal) coords = (rng.Next(0, 8), rng.Next(0, 8));
    static int emptyNodesFiredAt = 0;
    public static void Turn()
    {
        
    }


    // This method is used when the ships's orientation has been found.
    static void FinishingOffShip(Grid grids, bool checkList1) // Haven't yet accounted for when the first two coord pairs acquired belong to different ships.
    {
        bool validNodeFiredAt = false;
        int checkList = checkList1 ? 0 : 1;

        validNodeFiredAt = Shoot(grids.playerGrid, coordSearchQueue[checkList][0]);

        emptyNodesFiredAt = grids.playerGrid[coordSearchQueue[checkList][0].Item1, coordSearchQueue[checkList][0].Item2].NodeFilled == true 
        ? emptyNodesFiredAt : emptyNodesFiredAt + 1; // Adds one to emptyNodesFiredAt if it doesn't shoot at a ship segment.

        coordSearchQueue[checkList].RemoveAt(0);
    }



    static void CompareCoords()
    { 
        if (coordPairs[0].Item1 == coordPairs[1].Item1) // Find a way to not hard code this.
        {
            coordSearchQueue[0].Add((coordPairs[0].Item1, coordPairs[0].Item2 + 1));
            coordSearchQueue[0].Add((coordPairs[0].Item1, coordPairs[0].Item2 + 2));
            coordSearchQueue[0].Add((coordPairs[0].Item1, coordPairs[0].Item2 + 3));
            coordSearchQueue[1].Add((coordPairs[0].Item1, coordPairs[0].Item2 - 1));
            coordSearchQueue[1].Add((coordPairs[0].Item1, coordPairs[0].Item2 - 2));
            coordSearchQueue[1].Add((coordPairs[0].Item1, coordPairs[0].Item2 - 3));
        }
        else
        {
            coordSearchQueue[0].Add((coordPairs[0].Item1 + 1, coordPairs[0].Item2));
            coordSearchQueue[0].Add((coordPairs[0].Item1 + 2, coordPairs[0].Item2));
            coordSearchQueue[0].Add((coordPairs[0].Item1 + 3, coordPairs[0].Item2));
            coordSearchQueue[1].Add((coordPairs[0].Item1 - 1, coordPairs[0].Item2));
            coordSearchQueue[1].Add((coordPairs[0].Item1 - 2, coordPairs[0].Item2));
            coordSearchQueue[1].Add((coordPairs[0].Item1 - 3, coordPairs[0].Item2));
        }
        
    }



    // This method is for when one segment of a ship has been found. 
    // It is searching for the ship's orientation.
    static bool FindShipOrientation(Grid grids, (int vertical, int horizontal) coords)
    {
        coordPairs.Clear();
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