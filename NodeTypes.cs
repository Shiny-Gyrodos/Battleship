using System.Dynamic;
class Node(bool isMine, char icon, int shipType) // Needs an overhaul in the future.
{
    static string[] shipTypes = ["No Ship", "Submarine", "Destroyer", "Cruiser", "Battleship", "Carrier"];
    public bool IsMine { get; set; } = isMine;
    public char Icon { get; set; } = icon;
    public string ShipType = shipTypes[shipType];
    
    //public bool FiredAt = false; Potetially add later
}