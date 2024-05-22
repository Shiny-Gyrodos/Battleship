abstract class Ship
{
    static Random rng = new(); // Remove as many static variables as possible.
    static bool shipPlaced;
    static int coord1 = rng.Next(0, 8);
    static int coord2 = rng.Next(0, 8);
    static List<int> verticalIncrements = [];
    static List<int> horizontalIncrements = [];
    static Node[] nodeTypeStorage = [new Node(false, false, 'O', 0), new Node(true, false, 'H', 1), new Node(true, false, 'H', 2), new Node(true, false, 'H', 3), new Node(true, false, 'H', 4), new Node(true, false, 'H', 5)];
    public static void Place(Node[,] chosenGrid, int shipSegments)
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

            if (chosenGrid[coord1, coord2].NodeFilled)
            {
                ReinitializeVariables(true);
            }
            else
            {
                chosenGrid[coord1, coord2] = nodeTypeStorage[shipSegments];
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
                    isVertical = false;
                }
                else if (verticalIncrements.Count == 0)
                {
                    isVertical = true;
                }

                nodeValid = NodesValid(shipSegments, isVertical, chosenGrid);
            }

            chosenGrid[coord1, coord2] = !nodeValid ? nodeTypeStorage[0] : chosenGrid[coord1, coord2]; // If node wasn't valid it is set back to empty.
        }
    }



    static void PlaceShipNodes(int shipSegments, int chosenIncrement, bool isVertical, Node[,] chosenGrid)
    {
        for (int i = 0; i < shipSegments - 1; i++)
        {
            if (isVertical)
            {
                coord1 += chosenIncrement;
                chosenGrid[coord1, coord2] = nodeTypeStorage[shipSegments];
            }
            else
            {
                coord2 += chosenIncrement;
                chosenGrid[coord1, coord2] = nodeTypeStorage[shipSegments];
            }
        }

        verticalIncrements.Clear();
        horizontalIncrements.Clear();
        shipPlaced = true;
    }



    static bool NodesValid(int shipSegments, bool isVertical, Node[,] chosenGrid)
    {
        int emptyNodeCount = 0;

        if (isVertical)
        {
            int testCoord = coord1;
            int chosenIncrement = verticalIncrements[rng.Next(0, verticalIncrements.Count)];

            for(int i = 0; i < (shipSegments - 1); i++)
            {
                testCoord += chosenIncrement;
                try {emptyNodeCount = chosenGrid[testCoord, coord2].NodeFilled ? emptyNodeCount : emptyNodeCount + 1;}
                catch (IndexOutOfRangeException) {} // Try/catch blocks remove the need for extra calculations.
            }

            if (emptyNodeCount == shipSegments - 1)
            {
                PlaceShipNodes(shipSegments, chosenIncrement, isVertical, chosenGrid);
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
            int testCoord = coord2;
            int chosenIncrement = horizontalIncrements[rng.Next(0, horizontalIncrements.Count)];

            for(int i = 0; i < (shipSegments - 1); i++)
            {
                testCoord += chosenIncrement;
                try {emptyNodeCount = chosenGrid[coord1, testCoord].NodeFilled ? emptyNodeCount : emptyNodeCount + 1;}
                catch (IndexOutOfRangeException) {} // Try/catch blocks remove the need for extra calculations.
            }

            if (emptyNodeCount == shipSegments - 1)
            {
                PlaceShipNodes(shipSegments, chosenIncrement, isVertical, chosenGrid);
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
            coord1 = rng.Next(0, 8);
            coord2 = rng.Next(0, 8);
        }
        else
        {
            shipPlaced = false;
            verticalIncrements.Add(1);
            verticalIncrements.Add(-1);
            horizontalIncrements.Add(1);
            horizontalIncrements.Add(-1);
            coord1 = rng.Next(0, 8);
            coord2 = rng.Next(0, 8);
        }
    }
}