abstract class Attacks // Potentially change from abstract in the future.
{
    static Random rng = new();

    public static bool Shoot(Node[,] chosenGrid, int coord1, int coord2)
    {
        if (!chosenGrid[coord1, coord2].FiredAt)
        {
            switch (chosenGrid[coord1, coord2].NodeFilled)
            {
                case true: // If ship
                    chosenGrid[coord1, coord2] = new Node(false, true, 'X', NodeTypes.other);
                    break;
                case false: // If empty
                    chosenGrid[coord1, coord2] = new Node(false, true, '0', NodeTypes.other);
                    break;
                case null: // If mine
                    chosenGrid[coord1, coord2] = new Node(false, true, '#', NodeTypes.other);
                    break;
            }

            return true;
        }

        return false; // If else
    }



    public static void MineDetonates(Node[,] chosenGrid)
    {
        for (int validNodesFound = 0; validNodesFound < 2; )
        {
            int coord1 = rng.Next(0, 8);
            int coord2 = rng.Next(0, 8);

            // Shoot returns a boolean that determines whether it shot successfully at the spot specified or not.
            validNodesFound = Shoot(chosenGrid, coord1, coord2) ? validNodesFound + 1 : validNodesFound;
        }
    }
}