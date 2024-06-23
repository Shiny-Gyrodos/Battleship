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
        (List<int> vertical, List<int> horizontal) increments = ([1, -1], [1, -1]);
        bool firstCheckVertical = rng.Next(1, 3) > 1.5;

        for (int i = 0; i < increments.vertical.Count + increments.horizontal.Count; i++)
        {
            if (i < 2 && firstCheckVertical) // First checking vertical offsets. Added randomization would be a good idea.
            {
                if (!Shoot(grids.playerGrid, (coords.vertical + increments.vertical[i], coords.horizontal)))
                {
                    increments.vertical.Remove(i);
                }
            }
            else // Secondly checking horizontal offsets.
            {
                if (!Shoot(grids.playerGrid, (coords.vertical, coords.horizontal + increments.horizontal[i - 2])))
                {
                    increments.horizontal.Remove(i);
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