abstract class Ship
{
    static Random rng = new(); // Remove as many static variables as possible.
    static bool shipPlaced;
    static int coord1 = rng.Next(0, 8);
    static int coord2 = rng.Next(0, 8);
    static List<int> horizontalIncrements = [];
    static List<int> verticalIncrements = [];
    static Node[] nodeTypeStorage = [new Node(false, false, 'O', 0), new Node(true, false, 'H', 1), new Node(true, false, 'H', 2), new Node(true, false, 'H', 3), new Node(true, false, 'H', 4), new Node(true, false, 'H', 5)];
    public static void Place(Node[,] chosenGrid, int shipSegments)
    {
        ReinitializeVariables(false);
        bool nodeValid = true;

        while(!shipPlaced)
        {  
            //Console.WriteLine(18); // Debugging
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

            RemoveInvalidIncrements(shipSegments);

            while (horizontalIncrements.Count + verticalIncrements.Count > 0 && startingNodeEmpty) // Still loops infinitely sometimes
            {
                //Console.WriteLine(41); // Debugging
                bool isHorizontal = false;

                if (horizontalIncrements.Count > 0 && verticalIncrements.Count > 0) // Clean up if/else chain
                {
                    //Console.WriteLine(44); // Seems to enter into this if statement when bugging out.
                    isHorizontal = rng.Next(1, 3) > 1.5;
                }
                else if (horizontalIncrements.Count == 0)
                {
                    //Console.WriteLine(49);
                    isHorizontal = false;
                }
                else if (verticalIncrements.Count == 0)
                {
                    //Console.WriteLine(54);
                    isHorizontal = true;
                }

                nodeValid = NodesValid(shipSegments, isHorizontal, chosenGrid);
            }

            chosenGrid[coord1, coord2] = !nodeValid ? nodeTypeStorage[0] : chosenGrid[coord1, coord2]; // If node wasn't valid it is set back to empty.
        }
    }



    static void PlaceShipNodes(int shipSegments, int chosenIncrement, bool horizontal, Node[,] chosenGrid)
    {
        //Console.WriteLine("PlaceShipNodes"); // Doesn't make it here.

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
        //Console.WriteLine("NodesValid"); // Debugging
        int emptyNodeCount = 0;

        if (isHorizontal)
        {
            int testCoord = coord1;
            int chosenIncrement = horizontalIncrements[rng.Next(0, horizontalIncrements.Count)];

            for(int i = 0; i < (shipSegments - 1); i++)
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
            int chosenIncrement = verticalIncrements[rng.Next(0, verticalIncrements.Count)];

            for(int i = 0; i < (shipSegments - 1); i++)
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
            horizontalIncrements.Add(1);
            horizontalIncrements.Add(-1);
            verticalIncrements.Add(1);
            verticalIncrements.Add(-1);
            coord1 = rng.Next(0, 8);
            coord2 = rng.Next(0, 8);
        }
    }
}