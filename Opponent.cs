using System.Diagnostics;
using System.Linq.Expressions;

abstract class Opponent : Attacks
{
    static readonly Random rng = new();
    public static void TakeTurn(Grid grids, Brain brain)
    {
        switch (brain.turnPhase)
        {
            case 1:
                if (Loop(Shoot, GetRandomCoords, grids.playerGrid, brain) is ((int, int), bool) data1 && data1.shotShip)
                {
                    brain.turnPhase++;
                    brain.sucShots.Add(data1.coords);
                    brain.turnPhase = Ship.CheckForDestroyed(grids.playerGrid, brain.shipsLeft) ? brain.turnPhase : 1;
                }
                break;
            case 2:
                if (Loop(Shoot, GetAlteredCoords, grids.playerGrid, brain) is ((int, int), bool) data2 && data2.shotShip)
                {
                    brain.turnPhase++;
                    brain.sucShots.Add(data2.coords);
                    brain.turnPhase = Ship.CheckForDestroyed(grids.playerGrid, brain.shipsLeft) ? brain.turnPhase : 1;
                    AddPossibleCoords(brain);
                }
                break;
            case 3:
                if (!Loop(Shoot, GetQueuedCoords, grids.playerGrid, brain, 2).shotShip)
                {
                    brain.turnPhase = 1;
                }
                break;
        }


        
        static Point GetRandomCoords(Brain _) => new(rng.Next(0, 8), rng.Next(0, 8));

        // Returns the randomized coord pair with an alteration of 1 or -1 to one of them.
        static Point GetAlteredCoords(Brain brain) =>  rng.Next(1, 3) > 1.5 ? 
        brain.sucShots[0] with {Column = brain.sucShots[0].Column + rng.Next(1, 3) > 1.5 ? 1 : -1} :
        brain.sucShots[0] with {Row = brain.sucShots[0].Row + rng.Next(1, 3) > 1.5 ? 1 : -1};

        // Returns a random dequeue if both queues have the same amount of coords in them, else picks the one with the most and dequeues a pair.
        static Point GetQueuedCoords(Brain brain) => brain.searchQueue[0].Count == brain.searchQueue[1].Count ? brain.searchQueue[rng.Next(0, 2)].Dequeue() : 
        (brain.searchQueue[0].Count < brain.searchQueue[1].Count ? 
        brain.searchQueue[1].Dequeue() : brain.searchQueue[0].Dequeue());


        // Adds all possible coords the ship could be in.
        static void AddPossibleCoords(Brain brain)
        { 
            for (int i = 1; i < 4; i++)
            {
                brain.searchQueue[0].Enqueue(brain.sucShots[0].Column == brain.sucShots[1].Column ? new(brain.sucShots[0].Column, brain.sucShots[0].Row + i) : new(brain.sucShots[0].Column + i, brain.sucShots[0].Row));
                brain.searchQueue[1].Enqueue(brain.sucShots[0].Column == brain.sucShots[1].Column ? new(brain.sucShots[0].Column, brain.sucShots[0].Row + i * -1) : new(brain.sucShots[0].Column + i * -1, brain.sucShots[0].Row));
            }
        }
    }



    // Repeats the desired function until it succeeds or the amount of times specified is met.
    // If no limit is specified then it'll repeat until its attack function succeeds.
    public static (Point point, bool shotShip) Loop(Func<Node[,], Point, bool> miscAttackFunction, Func<Brain, Point> obtainCoordinates, Node[,] grid, Brain brain, int times = 0)
    {
        int counter = 0;
        Point point;

        do
        {
            point = obtainCoordinates(brain);
            counter += times == 0 ? -1 : 1;
        } while (!miscAttackFunction(grid, point) && counter < times);

        return (point, grid[point.Column, point.Row].NodeFilled == true);
    }
}



class Brain()
{
    public readonly List<NodeTypes>[] shipsLeft = [[], [], [], [], [], []];
    public readonly List<Point> sucShots = [];
    public readonly Queue<Point>[] searchQueue = [[], []]; // The first queue is for positively modified coords while the second is for negatively.
    public int turnPhase = 1;
}



record Point(int Column, int Row);