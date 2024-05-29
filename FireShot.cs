class FireShot // Maybe change from abstract later
{
    public static void Shoot(Node[,] chosenGrid, Grid grid, int coord1, int coord2) // Needs a lot of work. // <Maybe change from static later.
    {
        if (chosenGrid == grid.playerGrid)
        {
            if (chosenGrid[coord1, coord2].ShipType != "No Ship")
            {
                chosenGrid[coord1, coord2] = new Node(false, 'X', 0);
            }
            else
            {
                chosenGrid[coord1, coord2] = new Node(false, '0', 0);
            }
        }
        else
        {
            if (chosenGrid[coord1, coord2].ShipType != "No Ship")
            {
                grid.opponentGrid[coord1, coord2] = new Node(false, 'X', 0);
                grid.opponentGridHidden[coord1, coord2] = new Node(false, 'X', 0);
            }
            else
            {
                grid.opponentGrid[coord1, coord2] = new Node(false, '0', 0);
                grid.opponentGridHidden[coord1, coord2] = new Node(false, '0', 0);
            }
        }
    }
}