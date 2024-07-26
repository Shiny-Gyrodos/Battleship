/* General breakdown: 

   This code chooses a random unfilled node in a chosen gird (e. chosenGrid), and
   fills it with a ship node. Then a random coord is chosen to be modified (e. exampleGrid[coord1, coord2], either coord1 or coord2 will be modified,
   the isVertical variable controls which). The program will then step out from the original node in the new direction (e. exampleGrid[coord1 + 1, coord2])
   the ship's length - 1 amount of times; so the carrier (length of 5) would step out 4 times. The program counts how many valid spaces there are while
   stepping out. If the amount of valid spaces (e. emptyNodeCount) == the ship's length - 1 (e. shipSegments), then the ship is placed.
   If the program counts less valid spacs than required, the program changes back the orignal node modified/filled to empty then picks a new starting
   location, then tries the whole process over again. */

abstract class Ship
{
    #region Ship placement code

    static Random rng = new(); // Remove as many static variables as possible in the future.
    static bool shipPlaced;
    static (int vertical, int horizontal) coords = (rng.Next(0, 8), rng.Next(0, 8));
    static List<int> verticalIncrements = [];
    static List<int> horizontalIncrements = [];
    public static void Place(Node[,] chosenGrid, NodeTypes ship, bool isPlayerGrid)
    {
        ReinitializeVariables(false);
        bool nodeValid = true;

        while(!shipPlaced)
        {  
            bool startingNodeEmpty = false;

            if (verticalIncrements.Count + horizontalIncrements.Count == 0)
            {
                ReinitializeVariables(false);
            }

            if (chosenGrid[coords.vertical, coords.horizontal].NodeFilled == true)
            {
                ReinitializeVariables(true);
            }
            else
            {
                chosenGrid[coords.vertical, coords.horizontal] = isPlayerGrid ? new Node(true, false, 'H', ship) : new Node(true, false, 'O', ship);
                startingNodeEmpty = true;
            }

            while (horizontalIncrements.Count + verticalIncrements.Count > 0 && startingNodeEmpty)
            {
                bool isVertical = false;

                if (horizontalIncrements.Count > 0 && verticalIncrements.Count > 0) // Clean up if/else chain
                {
                    isVertical = rng.Next(1, 3) > 1.5;
                }
                else if (horizontalIncrements.Count == 0)
                {
                    isVertical = true;
                }
                else if (verticalIncrements.Count == 0)
                {
                    isVertical = false;
                }

                nodeValid = NodesValid(ship, isVertical, chosenGrid, isPlayerGrid);
            }

            // If node wasn't valid it is set back to empty.
            chosenGrid[coords.vertical, coords.horizontal] = !nodeValid ? new Node(false, false, 'O', NodeTypes.other) : chosenGrid[coords.vertical, coords.horizontal];
        }
    }



    static void PlaceShipNodes(NodeTypes ship, int chosenIncrement, bool isVertical, Node[,] chosenGrid, bool isPlayerGrid)
    {
        for (int i = 0; i < (int)ship - 1; i++)
        {
            if (isVertical)
            {
                coords.vertical += chosenIncrement;
            }
            else
            {
                coords.horizontal += chosenIncrement;
            }

            chosenGrid[coords.vertical, coords.horizontal] = isPlayerGrid ? new Node(true, false, 'H', ship) : new Node(true, false, 'O', ship);
        }

        verticalIncrements.Clear();
        horizontalIncrements.Clear();
        shipPlaced = true;
    }



    static bool NodesValid(NodeTypes shipSegments, bool isVertical, Node[,] chosenGrid, bool isPlayerGrid)
    {
        int emptyNodeCount = 0;

        if (isVertical)
        {
            int testCoord = coords.vertical;
            int chosenIncrement = verticalIncrements[rng.Next(0, verticalIncrements.Count)];

            for(int i = 0; i < ((int)shipSegments - 1); i++)
            {
                testCoord += chosenIncrement;
                try {emptyNodeCount = chosenGrid[testCoord, coords.horizontal].NodeFilled == true ? emptyNodeCount : emptyNodeCount + 1;}
                catch (IndexOutOfRangeException) {} // Try/catch blocks remove the need for extra calculations.
            }

            if (emptyNodeCount == (int)shipSegments - 1)
            {
                PlaceShipNodes(shipSegments, chosenIncrement, isVertical, chosenGrid, isPlayerGrid);
                return true;
            }
            else
            {
                verticalIncrements.Remove(chosenIncrement);
                return false;
            }
        }
        else
        {
            int testCoord = coords.horizontal;
            int chosenIncrement = horizontalIncrements[rng.Next(0, horizontalIncrements.Count)];

            for(int i = 0; i < ((int)shipSegments - 1); i++)
            {
                testCoord += chosenIncrement;
                try {emptyNodeCount = chosenGrid[coords.vertical, testCoord].NodeFilled == true ? emptyNodeCount : emptyNodeCount + 1;}
                catch (IndexOutOfRangeException) {} // Try/catch blocks remove the need for extra calculations.
            }

            if (emptyNodeCount == (int)shipSegments - 1)
            {
                PlaceShipNodes(shipSegments, chosenIncrement, isVertical, chosenGrid, isPlayerGrid);
                return true;
            }
            else
            {
                horizontalIncrements.Remove(chosenIncrement);
                return false;
            }
        }
    }



    static void ReinitializeVariables(bool randomizeCoords) // Resets variables for placing the next ship
    {
        if (randomizeCoords)
        {
            coords.vertical = rng.Next(0, 8);
            coords.horizontal = rng.Next(0, 8);
        }
        else
        {
            shipPlaced = false;
            verticalIncrements.Clear();
            horizontalIncrements.Clear();
            verticalIncrements.Add(1);
            verticalIncrements.Add(-1);
            horizontalIncrements.Add(1);
            horizontalIncrements.Add(-1);
            coords.vertical = rng.Next(0, 8);
            coords.horizontal = rng.Next(0, 8);
        }
    }



    public static void PlaceAll(Node[,] chosenGrid, bool isPlayerGrid)
    {
        for (int i = 1; i <= 5; i++)
        {
            Place(chosenGrid, (NodeTypes)i, isPlayerGrid);
        }
    }

    #endregion 
    #region Ships destroyed?

    public static bool CheckForDestroyed(Node[,] chosenGrid, ref int[] shipsLeftPrevious)
    {
        int[] shipsLeftUpdated = new int[6];

        // Sorts the node types left into their respective lists.
        foreach (Node node in chosenGrid)
        {
            shipsLeftUpdated[(int)node.ShipType]++;
        }

        // Checks if a ship has been sunk.
        for (int i = 1; i <= 5; i++)
        {
            if (shipsLeftUpdated[i] == 0 && shipsLeftPrevious[i] > 0)
            {
                Console.WriteLine("\n" + (NodeTypes)i + " has sunk!");
                shipsLeftPrevious = shipsLeftUpdated;
                return true;
            }
        }

        shipsLeftPrevious = shipsLeftUpdated;
        return false;
    }

    #endregion
}