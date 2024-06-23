class Opponent : Attacks
{
    static Random rng = new();
    static (int vertical, int horizontal) coords = (rng.Next(0, 8), rng.Next(0, 8));
    static int decisionNum = 0;
    public static void Turn()
    {
        switch (decisionNum)
        {
            case 0:
                //FireDefaultShot();
                break;
            case 1:
                //FindingShipPosition();
                break;
            case 2:
                break;
        }
    }



    static void FindingShipPosition(Grid grids, (int vertical, int horizontal) coords) // This method is for when one segment of a ship has been found. It is looking for the ship's orientation.
    {
        List<int> possibleOffsets = [1, -1, 1, -1];

        for (int i = 0; i < possibleOffsets.Count; i++)
        {
            if (i < 2) // First checking vertical offsets. Added randomization would be a good idea.
            {
                try
                {
                    if (grids.playerGrid[coords.vertical + possibleOffsets[i], coords.horizontal].FiredAt) // Could maybe use the return value of shoot method to do this.
                    {
                        possibleOffsets.Remove(possibleOffsets[i]);
                    }
                    else
                    {
                        Shoot(grids.playerGrid, coords);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    possibleOffsets.Remove(possibleOffsets[i]);
                }
            }
            else // Secondly checking horizontal offsets.
            {
                try
                {
                    if (grids.playerGrid[coords.vertical, coords.horizontal + possibleOffsets[i]].FiredAt) // Could maybe use the return value of shoot method to do this.
                    {
                        possibleOffsets.Remove(possibleOffsets[i]);
                    }
                    else
                    {
                        Shoot(grids.playerGrid, coords);
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    possibleOffsets.Remove(possibleOffsets[i]);
                }
            }
        }
    }



    static void AcquireCoords()
    {
        coords.vertical = rng.Next(0, 8);
        coords.horizontal = rng.Next(0, 8);
    }



    static void HuntingMode(Node[,] chosenGrid) // Searches randomly for ships.
    {
        bool validNodeFound = false;

        while (!validNodeFound)
        {
            AcquireCoords();
            validNodeFound = Shoot(chosenGrid, coords);
        }
    }
}