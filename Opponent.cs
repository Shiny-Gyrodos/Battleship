class Opponent : Attacks
{
    static Random rng = new();
    static (int vertical, int horizontal) coords = (rng.Next(0, 8), rng.Next(0, 8));
    public static void Turn()
    {
        
    }


    // This method is for when one segment of a ship has been found. 
    // It is searching for the ship's orientation.
    static (string, int) FindShipPosition(Grid grids, (int vertical, int horizontal) coords)
    {
        (List<int> vertical, List<int> horizontal) = ([1, -1], [1, -1]);

        while (true) // True since there is gauranteed to be a second ship segments if this method is called.
        {
            bool firstCheckVertical = rng.Next(1, 3) > 1.5;

            if (firstCheckVertical && vertical.Count > 0)
            {
                int randomIncrement = vertical[rng.Next(0, vertical.Count)];

                if (!Shoot(grids.playerGrid, (coords.vertical + vertical[randomIncrement], coords.horizontal))) 
                {     
                    vertical.Remove(randomIncrement); 
                }
                else
                {
                    return ("vertical", randomIncrement);
                }
            }
            else if (!firstCheckVertical && horizontal.Count > 0)
            {
                int randomIncrement = horizontal[rng.Next(0, horizontal.Count)];

                if (!Shoot(grids.playerGrid, (coords.vertical, coords.horizontal + horizontal[randomIncrement])))
                {
                    horizontal.Remove(randomIncrement);
                }
                else
                {
                    return ("horizontal", randomIncrement);
                }
            }
        }
    }



    // Searching for ships by randomly firing.
    static void HuntForShipSegments(Node[,] chosenGrid) // Searches randomly for ships.
    {
        while (!Shoot(chosenGrid, coords))
        {
            AcquireCoords();
        }
    }



    static void AcquireCoords()
    {
        (coords.vertical, coords.horizontal) = (rng.Next(0, 8), rng.Next(0, 8));
    }
}