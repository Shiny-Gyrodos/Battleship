abstract class NodeType
{
    public bool nodeFilled = true;
    public virtual void Place()
    {
        throw new NotImplementedException();
    }
}



class Empty : NodeType
{
    
    public char icon = 'O';
}



class Carrier : NodeType
{
    
    public const int segments = 5;
}



class BattleShip : NodeType
{
    public const int segments = 4;
}



class Cruiser : NodeType
{
    public const int segments = 3;
}



class Destroyer : NodeType
{
    public const int segments = 2;
}



class Submarine : NodeType
{
    static Random rng = new();
    public const int segments = 1;
    public override void Place()
    {
        bool validPositionFound = false;

        while (!validPositionFound)
        {
            int coord1 = rng.Next(0, 8);
            int coord2 = rng.Next(0, 8);

            if(!Grid.node[coord1, coord2].nodeFilled)
            {
                Grid.node[coord1, coord2] = new Submarine();
                validPositionFound = true;
            }
        }
    }
}