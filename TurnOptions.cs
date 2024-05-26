class TurnOptions
{
    static Random rng = new();

    public void FireShot(Node[,] chosenGrid, Grid grid, int coord1, int coord2) // Needs a lot of work.
    {
        if (chosenGrid == grid.playerGrid)
        {
            if (chosenGrid[coord1, coord2].ShipType != "No Ship")
            {
                chosenGrid[coord1, coord2] = new Node(true, false, 'X', 0);
            }
            else
            {
                chosenGrid[coord1, coord2] = new Node(true, false, '0', 0);
            }
        }
        else
        {
            if (chosenGrid[coord1, coord2].ShipType != "No Ship")
            {
                grid.opponentGrid[coord1, coord2] = new Node(true, false, 'X', 0);
                grid.opponentGridHidden[coord1, coord2] = new Node(true, false, 'X', 0);
            }
            else
            {
                grid.opponentGrid[coord1, coord2] = new Node(true, false, '0', 0);
                grid.opponentGridHidden[coord1, coord2] = new Node(true, false, '0', 0);
            }
        }
    }



    public void MineDetonates(Node[,] chosenGrid, Grid grid)
    {
        List<Node[,]> possibleGrids = [grid.playerGrid, grid.opponentGridHidden];
        possibleGrids.Remove(chosenGrid);

        int validNodesFound = 0;

        while (validNodesFound < 2)
        {
            int coord1 = rng.Next(0, 8);
            int coord2 = rng.Next(0, 8);

            Node nodeSelected = possibleGrids[0][coord1, coord2];

            if (nodeSelected.Icon == 'H' || nodeSelected.Icon == 'O')
            {
                validNodesFound++;
                FireShot(chosenGrid, grid, coord1, coord2);
            }
        }
    }
}