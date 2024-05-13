using System.Dynamic;

class Node(bool nodeFilled, bool isMine, char icon, int shipType)
{
    static string[] shipTypes = ["Empty", "Submarine", "Destroyer", "Cruiser", "Battleship", "Carrier"];
    public bool NodeFilled { get; set; } = nodeFilled;
    public bool IsMine { get; set; } = isMine;
    public char Icon { get; set; } = icon;
    public string ShipType = shipTypes[shipType];
}