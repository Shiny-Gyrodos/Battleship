using System.Drawing;
using Microsoft.VisualBasic;

abstract class Attacks
{
    static readonly Random rng = new();

    public static bool Shoot(ref Node[,] chosenGrid, Point point)
    {
        if (!chosenGrid[point.Row, point.Column].FiredAt)
        {
            switch (chosenGrid[point.Row, point.Column].NodeFilled)
            {
                case true: // If ship
                    chosenGrid[point.Row, point.Column] = new Node(true, true, 'X', NodeTypes.other);
                    break;
                case false: // If empty
                    chosenGrid[point.Row, point.Column] = new Node(false, true, '0', NodeTypes.other);
                    break;
                case null: // If mine
                    chosenGrid[point.Row, point.Column] = new Node(null, true, '#', NodeTypes.other);
                    MineDetonates(ref chosenGrid);
                    break;
            }

            return true;
        }

        return false; // If else
    }



    public static bool TryShoot(ref Node[,] chosenGrid, Point point)
    {
        try
        {
            Shoot(ref chosenGrid, point);
            return true;
        }
        catch (IndexOutOfRangeException)
        {
            return false;
        }
    }



    public static bool Nuke(ref Node[,] chosenGrid, Point point) // Shoots at a 3x3 area around the specified coords.
    {
        for (int i = -1; i <= 1; i++)
        {
            for (int j = -1; j <= 1; j++)
            {
                try 
                { 
                    if (chosenGrid[point.Row + i, point.Column + j].FiredAt == false)
                    {
                        Shoot(ref chosenGrid, point with {Row = point.Row + i, Column = point.Column + j});
                    }
                }
                catch (IndexOutOfRangeException) { }
            }
        }

        return true;
    }



    public static void StripBomb(ref Node[,] chosenGrid, (int value, bool isColumn) data) // Shoots all nodes on a specified row or column.
    {
        for (int i = 0; i < 8; i++)
        {
            Shoot(ref chosenGrid, data.isColumn ? new(data.value, i) : new(i, data.value));
        }
    }



    public static bool PlaceRadar(ref Node[,] chosenGrid, Point point)
    {
        // Parse checks to see if a radar already exists there.
        // Radars can't be placed on nodes you've already fired at.
        if (int.TryParse(chosenGrid[point.Row, point.Column].Icon.ToString(), out _) || chosenGrid[point.Row, point.Column].FiredAt)
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
                    if (chosenGrid[point.Row + i, point.Column + j].NodeFilled != false && !chosenGrid[point.Row + i, point.Column + j].FiredAt)  
                    {
                        detectedObjects++;
                    }    
                }
                catch (IndexOutOfRangeException) { }
            }
        }

        chosenGrid[point.Row, point.Column].Icon = (char)(detectedObjects + 48);
        return true;
    }



    public static bool PlaceMine(ref Node[,] chosenGrid, Point point)
    {
        if (chosenGrid[point.Row, point.Column].NodeFilled == false && chosenGrid[point.Row, point.Column].FiredAt == false)
        {
            chosenGrid[point.Row, point.Column] = new Node(null, false, '+', NodeTypes.other);
            return true;
        }

        return false;
    }



    static void MineDetonates(ref Node[,] chosenGrid)
    {
        for (int validNodesFound = 0; validNodesFound < 2; )
        {
            Point point = new(rng.Next(0, 8), rng.Next(0, 8));

            // Shoot returns a boolean that determines whether it shot successfully at the spot specified or not.
            validNodesFound = Shoot(ref chosenGrid, point) ? validNodesFound + 1 : validNodesFound;
        }
    }
}