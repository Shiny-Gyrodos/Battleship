abstract class FireShot // Maybe change from abstract later
{
    public static bool Shoot(Node[,] chosenGrid, int coord1, int coord2)
    {
        if (!chosenGrid[coord1, coord2].FiredAt)
        {
            switch (chosenGrid[coord1, coord2].NodeFilled)
            {
                case true: // If ship
                    chosenGrid[coord1, coord2] = new Node(false, true, 'X', NodeTypes.other);
                    break;
                case false: // If empty
                    chosenGrid[coord1, coord2] = new Node(false, true, '0', NodeTypes.other);
                    break;
                case null: // If mine
                    chosenGrid[coord1, coord2] = new Node(false, true, '#', NodeTypes.other);
                    break;
            }

            return true;
        }

        return false; // If else
    }
}