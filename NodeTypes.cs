using System.Dynamic;

class Node(bool nodeFilled, bool isMine, char icon, string shipName)
{
    public bool NodeFilled { get; set; } = nodeFilled;
    public bool IsMine { get; set; } = isMine;
    public char Icon { get; set; } = icon;
    public string ShipName { get; set; } = shipName;
}