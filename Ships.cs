abstract class Ship
{
    public virtual void Place()
    {
        throw new NotImplementedException();
    }
}



class Carrier : Ship
{
    public int segments = 5;
}



class BattleShip : Ship
{
    public int segments = 4;
}



class Cruiser : Ship
{
    public int segments = 3;
}



class Destroyer : Ship
{
    public int segments = 2;
}



class Submarine : Ship
{
    public int segments = 1;

    public override void Place()
    {
        throw new NotImplementedException();
    }
}