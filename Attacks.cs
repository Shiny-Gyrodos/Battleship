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
                    MineDetonates(chosenGrid);
                    break;
            }

            return true;
        }

        return false; // If else
    }



    public static void StripBomb(Node[,] chosenGrid)
    {
        bool validInputRecieved = false;

        while (!validInputRecieved)
        {
            Console.WriteLine("\nInput the key of the column or row you wish to strip bomb.");
            char playerInput = Console.ReadKey().KeyChar;

            // 49-56 are the char values of numbers 1-8. 65-72 are the char values of A-H.
            if (playerInput >= 49 && playerInput <= 56)
            {
                // Bombing a row,
                for (int i = 0; i < 8; i++)
                {
                    // Subtracting 48 brings the char value down to 0-7
                    Shoot(chosenGrid, i, playerInput - 48);
                }

                validInputRecieved = true;
            }
            else if (playerInput >= 65 && playerInput <= 72)
            {
                // Bombing a column
                for (int i = 0; i < 8; i++)
                {
                    // Subtracting 65 brings the char value down to 0-7
                    Shoot(chosenGrid, playerInput - 65, i);
                }

                validInputRecieved = true;
            }
        }
    }



    public static bool PlaceRadar(Node[,] chosenGrid, int coord1, int coord2)
    {
        // If statement is checking whether the spot chosen already has a radar placed on it or not.
        // 48 is the number 0 in the ASCII table, and 57 is the number 9. ASCII table link : https://www.asciitable.com
        // Radars can't be placed on nodes you've already fired at.
        if (chosenGrid[coord1, coord2].Icon >= 48 && chosenGrid[coord1, coord2].Icon <= 57 && !chosenGrid[coord1, coord2].FiredAt)
        {
            return false;
        }

        int detectedObjects = 0;

        // Searches a 3x3 grid around where the radar is placed for occupied nodes.
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                try 
                { 
                    if (chosenGrid[coord1 + i, coord2 + j].NodeFilled != false)
                    {
                        detectedObjects++;
                    }
                }
                catch (IndexOutOfRangeException) { }
            }
        }

        chosenGrid[coord1, coord2].Icon = (char)detectedObjects;
        return true;
    }



    static void MineDetonates(Node[,] chosenGrid)
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