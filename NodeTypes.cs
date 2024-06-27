using System.Dynamic;
public enum NodeTypes
{
    other,
    submarine,
    destroyer,
    cruiser, 
    battleship, 
    carrier
}



class Node(bool? nodeFilled, bool firedAt, char icon, NodeTypes nodeType)
{
    // True = ship, false = empty, null = mine. Yes, this isn't a good way of doing this, and I'll avoid repeating this in future projects.
    public bool? NodeFilled { get; set; } = nodeFilled;
    public bool FiredAt { get; set; } = firedAt;
    public char Icon { get; set; } = icon;
    public NodeTypes ShipType = nodeType;
}