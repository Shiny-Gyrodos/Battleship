using System.Diagnostics;
using System.Linq.Expressions;

class Opponent : Attacks
{
    // Consider looking into adding a constructor class.
    static List<NodeTypes>[] shipsLeft = [[], [], [], [], [], []];
    static List<(int, int)> successfulCoordPairsShotAt = [];
    static Queue<(int, int)>[] coordSearchQueue = [[], []]; // The first queue is for positively modified coords while the second is for negatively.
    static readonly Random rng = new();
    static (int column, int row) coords = (rng.Next(0, 8), rng.Next(0, 8));
    static int emptyNodesFiredAt = 0;
    static int turnPhase = 1;
    public static void Turn(Grid grids)
    {
        switch (turnPhase)
        {
            case 1:
                turnPhase = HuntForShipSegments(grids) ? turnPhase + 1 : turnPhase;
                turnPhase = Ship.CheckForDestroyed(grids.playerGrid, shipsLeft) == null ? turnPhase : 1;
                break;
            case 2:
                turnPhase = FindShipOrientation(grids) ? turnPhase + 1 : turnPhase;
                turnPhase = Ship.CheckForDestroyed(grids.playerGrid, shipsLeft) == null ? turnPhase : 1;
                break;
            case 3:
                if (coordSearchQueue[0].Count + coordSearchQueue[1].Count == 0)
                {
                    AddPossibleCoords();
                }
                if (emptyNodesFiredAt == 2 || Ship.CheckForDestroyed(grids.playerGrid, shipsLeft) == null)
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
        (int column, int row) coords;

        do
        {
            int randomQueue = rng.Next(0, 2);
            coords = coordSearchQueue[randomQueue].Dequeue();

            if (grids.playerGrid[coords.column, coords.row].NodeFilled != true)
            {
                emptyNodesFiredAt++;
                coordSearchQueue[randomQueue].Clear();
            }
        } while (!Shoot(grids.playerGrid, coords) || emptyNodesFiredAt < 2);
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
        (List<int> column, List<int> row) = ([1, -1], [1, -1]);

        while (true) // True since there is gauranteed to be a second ship segments if this method is called.
        {
            // Sets the random list to a random one if both have more than one element left in them, else it sets it to whichever one has elements left in it.
            List<int> randomList = rng.Next(1, 5) > 1.5 ?
            (column.Count > 0 ? ref column : ref row) : 
            (row.Count > 0 ? ref row : ref column);

            int randomIncrement = randomList[rng.Next(0, randomList.Count)];

            if (Shoot(grids.playerGrid, randomList == column ? (coords.column + randomIncrement, coords.row) : (coords.column, coords.row + randomIncrement))) 
            {    
                // Returns true if it hit a ship segment, false if not.
                return randomList == column ? 
                grids.playerGrid[coords.column + randomIncrement, coords.row].NodeFilled == true : 
                grids.playerGrid[coords.column, coords.row + randomIncrement].NodeFilled == true;
            }
    
            randomList.Remove(randomIncrement); 
        }
    }



    // Searching for ships by randomly firing.
    static bool HuntForShipSegments(Grid grids) // Searches randomly for ships.
    {
        do
        {
            (coords.column, coords.row) = (rng.Next(0, 8), rng.Next(0, 8));
        } while (!Shoot(grids.playerGrid, coords));

        return grids.playerGrid[coords.column, coords.row].NodeFilled == true;
    }
}