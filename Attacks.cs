using System.Drawing;
using Microsoft.VisualBasic;

abstract class Attacks // Potentially change from abstract in the future.
{
    static Random rng = new();

    public static bool Shoot(Node[,] chosenGrid, Point point)
    {
        try 
        {
            if (!chosenGrid[point.Column, point.Row].FiredAt)
            {
                switch (chosenGrid[point.Column, point.Row].NodeFilled)
                {
                    case true: // If ship
                        chosenGrid[point.Column, point.Row] = new Node(true, true, 'X', NodeTypes.other);
                        break;
                    case false: // If empty
                        chosenGrid[point.Column, point.Row] = new Node(false, true, '0', NodeTypes.other);
                        break;
                    case null: // If mine
                        chosenGrid[point.Column, point.Row] = new Node(null, true, '#', NodeTypes.other);
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



    public static void Nuke(Node[,] chosenGrid, Point point) // Shoots at a 3x3 area around the specified coords.
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                try 
                { 
                    if (chosenGrid[point.Column + i, point.Row + j].NodeFilled != false)
                    {
                        Shoot(chosenGrid, point);
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
                    Shoot(chosenGrid, new(i, playerInput - 48));
                }

                break;
            }
            else if (playerInput >= 65 && playerInput <= 72)
            {
                // Bombing a column
                for (int i = 0; i < 8; i++)
                {
                    // Subtracting 65 brings the char value down to 0-7
                    Shoot(chosenGrid, new(playerInput - 65, i));
                }

                break;
            }
        }
    }



    public static bool PlaceRadar(Node[,] chosenGrid, Point point)
    {
        // Parse checks to see if a radar already exists there.
        // Radars can't be placed on nodes you've already fired at.
        if (int.TryParse(chosenGrid[point.Column, point.Row].Icon.ToString(), out _) || chosenGrid[point.Column, point.Row].FiredAt)
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
                    if (chosenGrid[point.Column + i, point.Row + j].NodeFilled != false && !chosenGrid[point.Column + i, point.Row + j].FiredAt)  
                    {
                        detectedObjects++;
                    }    
                }
                catch (IndexOutOfRangeException) { }
            }
        }

        chosenGrid[point.Column, point.Row].Icon = (char)detectedObjects;
        return true;
    }



    public static bool PlaceMine(Node[,] chosenGrid, Point point)
    {
        if (chosenGrid[point.Column, point.Row].NodeFilled == false && chosenGrid[point.Column, point.Row].FiredAt == false)
        {
            chosenGrid[point.Column, point.Row] = new Node(null, false, '+', NodeTypes.other);
            return true;
        }

        return false;
    }



    static void MineDetonates(Node[,] chosenGrid)
    {
        for (int validNodesFound = 0; validNodesFound < 2; )
        {
            Point point = new(rng.Next(0, 8), rng.Next(0, 8));

            // Shoot returns a boolean that determines whether it shot successfully at the spot specified or not.
            validNodesFound = Shoot(chosenGrid, point) ? validNodesFound + 1 : validNodesFound;
        }
    }
}