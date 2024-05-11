class PlaceShip
{
    static Random rng = new(); // Remove as many static variables as possible.
    static bool shipPlaced = false;
    static int coord1 = rng.Next(0, 8);
    static int coord2 = rng.Next(0, 8);
    static List<int> horizontalIncrements = [1, -1];
    static List<int> verticalIncrements = [1, -1];
    static int possibleIncrements = horizontalIncrements.Count + verticalIncrements.Count;
    public void Place(NodeType[,] chosenGrid, int shipSegments)
    {
        NodeType[] shipStorage = [new Submarine(), new Destroyer(), new Cruiser(), new BattleShip(), new Carrier()];

        while(!shipPlaced)
        {  
            if (possibleIncrements == 0)
            {
                coord1 = rng.Next(0, 8);
                coord2 = rng.Next(0, 8);
            }

            chosenGrid[coord1, coord2] = chosenGrid[coord1, coord2].nodeFilled ? chosenGrid[coord1, coord2] : shipStorage[shipSegments - 1];

            RemoveInvalidIncrements(shipSegments);

            while (possibleIncrements > 0)
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

                NodesValid(shipSegments, horizontal, chosenGrid);
            }
        }
    }



    public void PlaceCurrentShip(int shipSegments, int chosenIncrement) // Name something more descriptive.
    {
        throw new NotImplementedException(); // Add code
    }



    public bool NodesValid(int shipSegments, bool horizontal, NodeType[,] chosenGrid)
    {
        int emptyNodeCount = 0;

        if (horizontal)
        {
            int chosenIncrement = rng.Next(0, horizontalIncrements.Count);

            for(int i = 0; i < shipSegments - 1; i++)
            {
                coord1 += chosenIncrement;
                
                emptyNodeCount = chosenGrid[coord1, coord2].nodeFilled ? emptyNodeCount : emptyNodeCount + 1;
            }

            if (emptyNodeCount == shipSegments - 1)
            {
                PlaceCurrentShip(shipSegments, chosenIncrement);
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
            int chosenIncrement = rng.Next(0, verticalIncrements.Count);

            for(int i = 0; i < shipSegments - 1; i++)
            {
                coord2 += chosenIncrement;

                emptyNodeCount = chosenGrid[coord1, coord2].nodeFilled ? emptyNodeCount : emptyNodeCount + 1;
            }

            if (emptyNodeCount == shipSegments - 1)
            {
                PlaceCurrentShip(shipSegments, chosenIncrement);
                return true;
            }
            else
            {
                verticalIncrements.Remove(chosenIncrement);
                return false;
            }
        }
    }



    public void RemoveInvalidIncrements(int shipSegments) // Prevents out-of-bounds exceptions for each individual ship.
    {
        if (coord1 + shipSegments - 1 > 7)
        {
            horizontalIncrements.Remove(1);
        }
        else if (coord1 - shipSegments - 1 < 0)
        {
            horizontalIncrements.Remove(-1);
        }

        if (coord2 + shipSegments - 1 > 7)
        {
            verticalIncrements.Remove(1);
        }
        else if (coord2 + shipSegments - 1 < 0)
        {
            verticalIncrements.Remove(1);
        }
    }
}