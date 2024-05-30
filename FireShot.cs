class FireShot // Maybe change from abstract later
{
    public static void Shoot(Node[,] chosenGrid, Grid grid, int coord1, int coord2) // Maybe change from static later.
    {
        chosenGrid[coord1, coord2] = chosenGrid[coord1, coord2].NodeFilled == true ? new Node(true, 'X', 0) : new Node(true, '0', 0);
    }



    public static bool IsNodeValid(Node[,] chosenGrid, int coord1, int coord2) // Maybe change from static later.
    {
        return chosenGrid[coord1, coord2].Icon == 'X' || chosenGrid[coord1, coord2].Icon == '0' ? false : true;
    }
}