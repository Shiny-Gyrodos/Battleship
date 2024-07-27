using System.ComponentModel;
using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Principal;
using MyApp;
class Brain
{
    public int tokens = 0;
    public int[] shipsLeft = new int[6];
    public readonly List<Point> sucShots = new(2); // Stores coords successfully shot at for comparison.
    public Queue<Point> searchQueue = []; // Stores coords to shoot at
    public int turnPhase = 0;

    public Brain(Node[,] playerGrid)
    {
        foreach (Node node in playerGrid)
        {
            shipsLeft[(int)node.ShipType]++;
        }
    }
}



abstract class Opponent : Attacks
{
    delegate Point GetCoords(Brain brain);
    delegate bool Attack(ref Node[,] chosenGrid, Point point);
    static readonly Random rng = new();
    static readonly GetCoords[] decisionTree = [GetRandomCoords, GetAlteredCoords, GetQueuedCoords];



    public static bool TakeTurn(ref Grid grids, ref Brain brain)
    {
        brain.turnPhase = brain.turnPhase == 2 && brain.searchQueue.Count == 0 ? 0 : brain.turnPhase;

        if (LoopUntilExecution(brain.tokens >= 8 ? Nuke : Shoot, decisionTree[brain.turnPhase], ref grids.player, ref brain) is (Point, bool) data && data.shotShip)
        {
            if (brain.searchQueue.Count == 0 && brain.turnPhase == 2)
            {
                brain.turnPhase = 0;
            }

            if (brain.turnPhase != 2)
            {
                brain.sucShots.Add(data.point);
            }

            if (brain.turnPhase == 1)
            {
                AddPossibleCoords(ref brain);
            }

            if (Ship.CheckForDestroyed(grids.player, ref brain.shipsLeft))
            {
                brain.turnPhase = 0;
                brain.searchQueue.Clear();
            }
            else
            {
                brain.turnPhase++;
            }
        }

        return brain.shipsLeft[1] + brain.shipsLeft[2] + brain.shipsLeft[3] + brain.shipsLeft[4] + brain.shipsLeft[5] > 0;
    }



    static Point GetRandomCoords(Brain _) => new(rng.Next(0, 8), rng.Next(0, 8));



    // Returns the randomized coord pair with an alteration of 1 or -1 to one of them.
    // Add code that makes sure that coords aren't out of the boundsof the array.
    static Point GetAlteredCoords(Brain brain) => rng.Next(1, 3) > 1.5 ? 
    brain.sucShots[0] with {Column = brain.sucShots[0].Column + ReturnRandom(1, -1)} :
    brain.sucShots[0] with {Row = brain.sucShots[0].Row + ReturnRandom(1, -1)};



    static T ReturnRandom<T>(params T[] items) => items[rng.Next(0, items.Length - 1)];



    static Point GetQueuedCoords(Brain brain) => brain.searchQueue.Dequeue();



    // Adds all possible coords the ship could be in.
    static void AddPossibleCoords(ref Brain brain)
    { 
        for (int i = 1; i < 4; i++)
        {
            brain.searchQueue.Enqueue(GetAlteredCoords(brain, i));
            brain.searchQueue.Enqueue(GetAlteredCoords(brain, i, -1));
        }

        Queue<Point> filteredQueue = new();

        foreach(Point point in brain.searchQueue)
        {
            if (point.Column >= 0 && point.Column <= 7 && point.Row >= 0 && point.Row <= 7)
            {
                filteredQueue.Enqueue(point);
            }
        }

        brain.searchQueue = new(filteredQueue);

        // Helper function eliminates ternary operator mess in the for() loop.
        static Point GetAlteredCoords(Brain brain, int alteration, int multiplier = 1)
        {
            if (brain.sucShots[0].Column == brain.sucShots[1].Column)
            {
                return new(brain.sucShots[0].Column, (brain.sucShots[0].Row + alteration) * multiplier);
            }
            else
            {
                return new((brain.sucShots[0].Column + alteration) * multiplier, brain.sucShots[0].Row);
            }
        }
    }



    // Repeats the desired function until it succeeds.
    static (Point point, bool shotShip) LoopUntilExecution(Attack attack, GetCoords obtainCoords, ref Node[,] grid, ref Brain brain)
    {
        Point point;

        do
        {
            point = obtainCoords(brain);
        } while (!attack(ref grid, point));

        if (attack == Shoot)
        {
            brain.tokens++;
        }

        return (point, grid[point.Column, point.Row].NodeFilled == true);
    }
}