abstract class Ship
{
    static Random rng = new(); // Remove as many static variables as possible.
    static bool shipPlaced;
    static int ranCoord1 = rng.Next(0, 8);
    static int ranCoord2 = rng.Next(0, 8);
    static int coord1 = ranCoord1;
    static int coord2 = ranCoord2;
    static List<int> horizontalIncrements = [];
    static List<int> verticalIncrements = [];
    static Node[] nodeTypeStorage = [new Node(false, false, 'O', 0), new Node(true, false, 'H', 1), new Node(true, false, 'H', 2), new Node(true, false, 'H', 3), new Node(true, false, 'H', 4), new Node(true, false, 'H', 5)];
    public static void Place(Node[,] chosenGrid, int shipSegments)
    {
        ReinitializeVariables(true);
        ReinitializeVariables(false);
        bool nodeValid = true;

        while(!shipPlaced)
        {  
            //Console.WriteLine(18); // Debugging
            bool startingNodeValid = false;

            if (verticalIncrements.Count + horizontalIncrements.Count == 0)
            {
                ReinitializeVariables(true);
            }

            if (chosenGrid[coord1, coord2].NodeFilled)
            {
                ReinitializeVariables(true);
            }
            else
            {
                chosenGrid[coord1, coord2] = nodeTypeStorage[shipSegments];
                startingNodeValid = true;
            }

            RemoveInvalidIncrements(shipSegments);

            while (horizontalIncrements.Count + verticalIncrements.Count > 0 && startingNodeValid) // Still sometimes loops infinitely
            {
                //Console.WriteLine(41); // Debugging
                bool isHorizontal = false;

                if (horizontalIncrements.Count > 0 && verticalIncrements.Count > 0) // Clean up if/else chain
                {
                    isHorizontal = rng.Next(1, 3) > 1.5;
                }
                else if (horizontalIncrements.Count == 0)
                {
                    isHorizontal = false;
                }
                else if (verticalIncrements.Count == 0)
                {
                    isHorizontal = true;
                }

                nodeValid = NodesValid(shipSegments, isHorizontal, chosenGrid);
            }

            chosenGrid[ranCoord1, ranCoord2] = !nodeValid ? nodeTypeStorage[0] : chosenGrid[ranCoord1, ranCoord2]; // If node wasn't valid it is set back to empty.
        }
    }



    static void PlaceShipNodes(int shipSegments, int chosenIncrement, bool horizontal, Node[,] chosenGrid)
    {
        for (int i = 0; i < shipSegments - 1; i++)
        {
            if (horizontal)
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

        horizontalIncrements.Clear();
        verticalIncrements.Clear();
        shipPlaced = true;
    }



    static bool NodesValid(int shipSegments, bool isHorizontal, Node[,] chosenGrid)
    {
        int emptyNodeCount = 0;

        if (isHorizontal)
        {
            int testCoord = coord1;
            int chosenIncrement = rng.Next(0, horizontalIncrements.Count);

            for(int i = 0; i < shipSegments - 1; i++)
            {
                testCoord += chosenIncrement;
                emptyNodeCount = chosenGrid[testCoord, coord2].NodeFilled ? emptyNodeCount : emptyNodeCount + 1;
            }

            if (emptyNodeCount == shipSegments - 1)
            {
                PlaceShipNodes(shipSegments, chosenIncrement, isHorizontal, chosenGrid);
                return true;
            }
            else
            {
                horizontalIncrements.Remove(chosenIncrement);
                return false;
            }
        }
        else
        {
            int testCoord = coord2;
            int chosenIncrement = rng.Next(0, verticalIncrements.Count);

            for(int i = 0; i < shipSegments - 1; i++)
            {
                testCoord += chosenIncrement;
                emptyNodeCount = chosenGrid[coord1, testCoord].NodeFilled ? emptyNodeCount : emptyNodeCount + 1;
            }

            if (emptyNodeCount == shipSegments - 1)
            {
                PlaceShipNodes(shipSegments, chosenIncrement, isHorizontal, chosenGrid);
                return true;
            }
            else
            {
                verticalIncrements.Remove(chosenIncrement);
                return false;
            }
        }
    }



    static void RemoveInvalidIncrements(int shipSegments) // Prevents out-of-bounds exceptions for each individual ship.
    {
        if (coord1 + (shipSegments - 1) > 7)
        {
            horizontalIncrements.Remove(1);
        }
        else if (coord1 - (shipSegments - 1) < 0)
        {
            horizontalIncrements.Remove(-1);
        }

        if (coord2 + (shipSegments - 1) > 7)
        {
            verticalIncrements.Remove(1);
        }
        else if (coord2 + (shipSegments - 1) < 0)
        {
            verticalIncrements.Remove(1);
        }
    }



    static void ReinitializeVariables(bool randomizeCoords)
    {
        if (randomizeCoords)
        {
            ranCoord1 = rng.Next(0, 8);
            ranCoord2 = rng.Next(0, 8);
            coord1 = ranCoord1;
            coord2 = ranCoord2;
        }
        else
        {
            shipPlaced = false;
            horizontalIncrements.Add(1);
            horizontalIncrements.Add(-1);
            verticalIncrements.Add(1);
            verticalIncrements.Add(-1);
            ranCoord1 = rng.Next(0, 8);
            ranCoord2 = rng.Next(0, 8);
        }
    }
}