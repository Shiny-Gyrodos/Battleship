abstract class Ship
{
    static Random rng = new(); // Remove as many static variables as possible.
    static bool shipPlaced = false;
    static int ranCoord1 = rng.Next(0, 8);
    static int randCoord2 = rng.Next(0, 8);
    static int coord1 = ranCoord1;
    static int coord2 = randCoord2;
    static List<int> horizontalIncrements = [1, -1];
    static List<int> verticalIncrements = [1, -1];
    static int possibleIncrements = horizontalIncrements.Count + verticalIncrements.Count;
    static Node[] nodeTypeStorage = [new Node(false, false, ' ', 0), new Node(true, false, 'H', 1), new Node(true, false, 'H', 2), new Node(true, false, 'H', 3), new Node(true, false, 'H', 4), new Node(true, false, 'H', 5)];
    public static void Place(Node[,] chosenGrid, int shipSegments)
    {
        bool nodeValid = true;

        if (horizontalIncrements.Count == 0) // Clean up later
        {
            horizontalIncrements.Add(1);
            horizontalIncrements.Add(-1);
        }
        if (verticalIncrements.Count == 0)
        {
            verticalIncrements.Add(1);
            verticalIncrements.Add(-1);
        }

        while(!shipPlaced) // Repeats infinitely
        {  
            if (possibleIncrements == 0)
            {
                coord1 = rng.Next(0, 8);
                coord2 = rng.Next(0, 8);
            }

            chosenGrid[coord1, coord2] = chosenGrid[coord1, coord2].NodeFilled ? chosenGrid[coord1, coord2] : nodeTypeStorage[shipSegments];

            RemoveInvalidIncrements(shipSegments);

            while (possibleIncrements > 0) // Repeats infinitely
            {
                bool horizontal = false;

                if (horizontalIncrements.Count > 0 && verticalIncrements.Count > 0) // Clean up if/else chain
                {
                    horizontal = rng.Next(1, 3) > 1.5;
                }
                else if (horizontalIncrements.Count == 0)
                {
                    horizontal = false;
                }
                else if (verticalIncrements.Count == 0)
                {
                    horizontal = true;
                }

                nodeValid = NodesValid(shipSegments, horizontal, chosenGrid);
            }
        }

        if (!nodeValid) // If node wasn't valid it is set back to empty. Clean up later
        {
            chosenGrid[ranCoord1, randCoord2] = nodeTypeStorage[0];
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



    static bool NodesValid(int shipSegments, bool horizontal, Node[,] chosenGrid)
    {
        int emptyNodeCount = 0;

        if (horizontal)
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
                PlaceShipNodes(shipSegments, chosenIncrement, horizontal, chosenGrid);
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
                PlaceShipNodes(shipSegments, chosenIncrement, horizontal, chosenGrid);
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
}