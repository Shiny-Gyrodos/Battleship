abstract class Attacks // Potentially change from abstract in the future.
{
    static Random rng = new();

    public static bool Shoot(Node[,] chosenGrid, (int vertical, int horizontal) coords)
    {
        try 
        {
            if (!chosenGrid[coords.vertical, coords.horizontal].FiredAt)
            {
                switch (chosenGrid[coords.vertical, coords.horizontal].NodeFilled)
                {
                    case true: // If ship
                        chosenGrid[coords.vertical, coords.horizontal] = new Node(true, true, 'X', NodeTypes.other);
                        break;
                    case false: // If empty
                        chosenGrid[coords.vertical, coords.horizontal] = new Node(false, true, '0', NodeTypes.other);
                        break;
                    case null: // If mine
                        chosenGrid[coords.vertical, coords.horizontal] = new Node(null, true, '#', NodeTypes.other);
                        MineDetonates(chosenGrid);
                        break;
                }

                return true;
            }
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }

        return false; // If else
    }



    public static void Nuke(Node[,] chosenGrid, (int vertical, int horizontal) coords) // Shoots at a 3x3 area around the specified coords.
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                try 
                { 
                    if (chosenGrid[coords.vertical + i, coords.horizontal + j].NodeFilled != false)
                    {
                        Shoot(chosenGrid, coords);
                    }
                }
                catch (IndexOutOfRangeException) { }
            }
        }
    }



    public static void StripBomb(Node[,] chosenGrid) // Shoots all nodes on a specified row or column.
    {
        while (true)
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
                    Shoot(chosenGrid, (i, playerInput - 48));
                }

                break;
            }
            else if (playerInput >= 65 && playerInput <= 72)
            {
                // Bombing a column
                for (int i = 0; i < 8; i++)
                {
                    // Subtracting 65 brings the char value down to 0-7
                    Shoot(chosenGrid, (playerInput - 65, i));
                }

                break;
            }
        }
    }



    public static bool PlaceRadar(Node[,] chosenGrid, (int vertical, int horizontal) coords)
    {
        // If statement is checking whether the spot chosen already has a radar placed on it or not.
        // 48 is the number 0 in the ASCII table, and 57 is the number 9. ASCII table link : https://www.asciitable.com
        // Radars can't be placed on nodes you've already fired at.
        if (chosenGrid[coords.vertical, coords.horizontal].Icon >= 48 && chosenGrid[coords.vertical, coords.horizontal].Icon <= 57 && !chosenGrid[coords.vertical, coords.horizontal].FiredAt)
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
                    if (chosenGrid[coords.vertical + i, coords.horizontal + j].NodeFilled != false)
                    {
                        detectedObjects++;
                    }
                }
                catch (IndexOutOfRangeException) { }
            }
        }

        chosenGrid[coords.vertical, coords.horizontal].Icon = (char)detectedObjects;
        return true;
    }



    public static bool PlaceMine(Node[,] chosenGrid, (int vertical, int horizontal) coords)
    {
        if (chosenGrid[coords.vertical, coords.horizontal].NodeFilled == false)
        {
            chosenGrid[coords.vertical, coords.horizontal] = new Node(null, chosenGrid[coords.vertical, coords.horizontal].FiredAt, '+', NodeTypes.other);
            return true;
        }

        return false;
    }



    static void MineDetonates(Node[,] chosenGrid)
    {
        for (int validNodesFound = 0; validNodesFound < 2; )
        {
            (int vertical, int horizontal) coords = (rng.Next(0, 8), rng.Next(0, 8));

            // Shoot returns a boolean that determines whether it shot successfully at the spot specified or not.
            validNodesFound = Shoot(chosenGrid, coords) ? validNodesFound + 1 : validNodesFound;
        }
    }
}