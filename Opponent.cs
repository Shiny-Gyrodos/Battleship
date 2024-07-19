using System.Diagnostics;
using System.Linq.Expressions;

class Opponent : Attacks
{
    // Consider looking into adding a constructor class.
    static List<NodeTypes>[] shipsLeft = [[], [], [], [], [], []];
    static List<(int, int)> sucShots = [];
    static Queue<(int, int)>[] coordSearchQueue = [[], []]; // The first queue is for positively modified coords while the second is for negatively.
    static readonly Random rng = new();
    static (int column, int row) coords = (rng.Next(0, 8), rng.Next(0, 8));
    static int emptyNodesFiredAt = 0;
    static int turnPhase = 1;
    public static void TakeTurn(Grid grids)
    {
        switch (turnPhase)
        {
            case 1:
                if (FireRandomly(grids))
                {
                    turnPhase++;
                    sucShots.Add(coords);
                    turnPhase = Ship.CheckForDestroyed(grids.playerGrid, shipsLeft) ? turnPhase : 1; // Null is placeholder
                }
                break;
            case 2:
                if (FindShipRotation(grids))
                {
                    turnPhase++;
                    sucShots.Add(coords);
                    turnPhase = Ship.CheckForDestroyed(grids.playerGrid, shipsLeft) ? turnPhase : 1; // Null is placeholder
                }
                break;
            case 3:
                if (coordSearchQueue[0].Count + coordSearchQueue[1].Count == 0)
                {
                    AddPossibleCoords();
                }
                if (emptyNodesFiredAt == 2 || Ship.CheckForDestroyed(grids.playerGrid, shipsLeft))
                {
                    turnPhase = 1;
                    break;
                }
                ShootAtFoundShip(grids);
                break;
        }



        // Adds all possible coords the ships could be in.
        static void AddPossibleCoords()
        { 
            for (int i = 1; i < 4; i++)
            {
                coordSearchQueue[0].Enqueue(sucShots[0].Item1 == sucShots[1].Item1 ? (sucShots[0].Item1, sucShots[0].Item2 + i) : (sucShots[0].Item1 + i, sucShots[0].Item2));
                coordSearchQueue[1].Enqueue(sucShots[0].Item1 == sucShots[1].Item1 ? (sucShots[0].Item1, sucShots[0].Item2 + i * -1) : (sucShots[0].Item1 + i * -1, sucShots[0].Item2));
            }
        }
    }


    // This method is used when the ships's orientation has been found.
    static void ShootAtFoundShip(Grid grids) // Could be improved by accounting for when the ai finds two ships next to each other.
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
        } while (emptyNodesFiredAt < 2 || !Shoot(grids.playerGrid, coords));
    }



    // This method is for when one segment of a ship has been found. 
    static bool FindShipRotation(Grid grids)
    {
        (List<int> column, List<int> row) = ([1, -1], [1, -1]);

        while (true) // True since there is gauranteed to be a second ship segments if this method is called.
        {
            // Sets randomList to a random one if both have more than one element left in them, else it sets it to whichever one has elements left in it.
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
    static bool FireRandomly(Grid grids) // Searches randomly for ships.
    {
        do
        {
            (coords.column, coords.row) = (rng.Next(0, 8), rng.Next(0, 8));
        } while (!Shoot(grids.playerGrid, coords));

        return grids.playerGrid[coords.column, coords.row].NodeFilled == true;
    }
}