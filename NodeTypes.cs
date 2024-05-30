using System.Dynamic;
class Node(bool nodeFilled, char icon, int shipType) // Needs an overhaul in the future.
{
    static string[] shipTypes = ["Other", "Submarine", "Destroyer", "Cruiser", "Battleship", "Carrier"];
    public bool? NodeFilled { get; set; } = nodeFilled; // True = ship, false = empty, null = mine.
    public char Icon { get; set; } = icon;
    public string ShipType = shipTypes[shipType];
    
    //public bool FiredAt = false; Potetially add later
}