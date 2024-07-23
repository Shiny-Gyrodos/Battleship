using System.Diagnostics;
using System.Linq.Expressions;
using System.Security.Principal;
using MyApp;
class Brain()
{
    public List<NodeTypes>[] shipsLeft = [[], [], [], [], [], []];
    public readonly List<Point> sucShots = new(2);
    public Queue<Point> searchQueue = []; // The first queue is for positively modified coords while the second is for negatively.
    public int turnPhase = 0;
}



abstract class Opponent : Attacks
{
    delegate Point GetCoords(Brain brain);
    delegate bool Attack(Node[,] chosenGrid, Point point);
    static readonly Random rng = new();
    static readonly GetCoords[] decisionTree = [GetRandomCoords, GetAlteredCoords, GetQueuedCoords];
    public static void TakeTurn(Grid grids, ref Brain brain)
    {
        brain.turnPhase = brain.turnPhase == 2 && brain.searchQueue.Count == 0 ? 0 : brain.turnPhase;

        if (LoopUntilExecution(Shoot, decisionTree[brain.turnPhase], grids.player, brain) is (Point, bool) data && data.shotShip)
        {
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
    }



    static Point GetRandomCoords(Brain _) => new(rng.Next(0, 8), rng.Next(0, 8));



    // Returns the randomized coord pair with an alteration of 1 or -1 to one of them.
    static Point GetAlteredCoords(Brain brain) => rng.Next(1, 3) > 1.5 ? 
    brain.sucShots[0] with {Column = brain.sucShots[0].Column + rng.Next(1, 3) > 1.5 ? 1 : -1} :
    brain.sucShots[0] with {Row = brain.sucShots[0].Row + rng.Next(1, 3) > 1.5 ? 1 : -1};



    // Returns a random dequeue if both queues have the same amount of coords in them, else picks the one with the most and dequeues a pair.
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
                brain.searchQueue.Enqueue(point);
            }
        }

        brain.searchQueue = new(filteredQueue);

        // Helper function eliminates ternary operator mess on lines 72 and 73.
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
    static (Point point, bool shotShip) LoopUntilExecution(Attack attack, GetCoords obtainCoords, Node[,] grid, Brain brain)
    {
        Point point;

        do
        {
            point = obtainCoords(brain);
        } while (!attack(grid, point));

        return (point, grid[point.Column, point.Row].NodeFilled == true);
    }
}