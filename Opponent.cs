using System.Diagnostics;
using System.Linq.Expressions;

class Opponent : Attacks
{
    // Consider looking into adding a constructor class.
    static List<(int, int)> successfulCoordPairsShotAt = [];
    static Queue<(int, int)>[] coordSearchQueue = [[], []]; // The first queue is for positively modified coords while the second is for negatively.
    static Random rng = new();
    static (int vertical, int horizontal) coords = (rng.Next(0, 8), rng.Next(0, 8));
    static int emptyNodesFiredAt = 0;
    static int turnPhase = 1;
    public static void Turn(Grid grids)
    {
        switch (turnPhase)
        {
            case 1:
                turnPhase = HuntForShipSegments(grids) ? turnPhase + 1 : turnPhase;
                turnPhase = Ship.CheckForDestroyed() == null ? turnPhase : 1;
                break;
            case 2:
                turnPhase = FindShipOrientation(grids) ? turnPhase + 1 : turnPhase;
                turnPhase = Ship.CheckForDestroyed() == null ? turnPhase : 1;
                break;
            case 3:
                if (coordSearchQueue[0].Count + coordSearchQueue[1].Count == 0)
                {
                    AddPossibleCoords();
                }

                if (emptyNodesFiredAt == 2 || Ship.CheckForDestroyed() == null)
                {
                    turnPhase = 1;
                    break;
                }
                FinishingOffShip(grids);
                break;
        }
    }


    // This method is used when the ships's orientation has been found.
    static void FinishingOffShip(Grid grids) // Could be improved by accounting for when the ai finds two ships next to each other.
    {
        bool validNodeFiredAt = false;

        while (!validNodeFiredAt || emptyNodesFiredAt < 2)
        {
            int randomQueue = rng.Next(0, 2);
            (int, int) coordPair = coordSearchQueue[randomQueue].Dequeue();

            validNodeFiredAt = Shoot(grids.playerGrid, coordPair);

            if (grids.playerGrid[coordPair.Item1, coordPair.Item2].NodeFilled != true)
            {
                emptyNodesFiredAt++;
                coordSearchQueue[randomQueue].Clear();
            }
        }
    }



    static void AddPossibleCoords()
    { 
        for (int i = 0; i < 2; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (successfulCoordPairsShotAt[0].Item1 == successfulCoordPairsShotAt[1].Item1)
                {
                    // If i == 1, then make the coord negative.
                    coordSearchQueue[i].Enqueue((successfulCoordPairsShotAt[0].Item1, (successfulCoordPairsShotAt[0].Item2 + j + 1) * i == 1 ? -1 : 1));
                }
                else
                {
                    // Same as above, but altering the other coord.
                    coordSearchQueue[i].Enqueue(((successfulCoordPairsShotAt[0].Item1 + j + 1) * i == 1 ? -1 : 1, successfulCoordPairsShotAt[0].Item2));
                }
            }
        } 
    }



    // This method is for when one segment of a ship has been found. 
    static bool FindShipOrientation(Grid grids)
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
    static bool HuntForShipSegments(Grid grids) // Searches randomly for ships.
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