abstract class Abilities // Potentially change from abstract in the future.
{
    static Random rng = new();

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
                FireShot.Shoot(chosenGrid, grid, coord1, coord2);
            }
        }
    }
}