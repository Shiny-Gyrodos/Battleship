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



class Node(bool nodeFilled, bool firedAt, char icon, NodeTypes nodeType) // Needs an overhaul in the future.
{
    public bool? NodeFilled { get; set; } = nodeFilled; // True = ship, false = empty, null = mine.
    public bool FiredAt { get; set; } = firedAt;
    public char Icon { get; set; } = icon;
    public NodeTypes ShipType = nodeType;
}