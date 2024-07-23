using System.Dynamic;
using System.Security.Cryptography;
class PlayerData
{
    public int Tokens;
    public PlayerData()
    {
        Tokens = 0;
        int.Clamp(Tokens, 0, 10);
    }
}



class Player : Attacks
{
    delegate bool Option(Node[,] chosenGrid, Point point);
    public static void TakeTurn(Grid grids, PlayerData player)
    {
        bool validChoiceMade = false;

        while (!validChoiceMade)
        {
            Console.Write("\nChoose a firing mode:\nShoot - 0 Cost | Strip Bomb - 8 Cost | Nuke - 8 Cost | Radar - 3 Cost | Place Mine - 1 Cost");
            string playerChoice = Console.ReadLine().ToLower();

            switch ((playerChoice, player.Tokens))
            {
                case ("shoot", _):
                    validChoiceMade = LoopWithMessage(Shoot, GetPlayerInput, grids.player, "You can't shoot there. Try somewhere else.");
                    player.Tokens++;
                    break;
                case ("strip bomb", >= 8): // Succeeds no matter what
                    validChoiceMade = true;
                    //StripBomb();
                    break;
                case ("nuke", >= 8): // Succeeds no matter what
                    validChoiceMade = true;
                    Nuke(grids.opponent, GetPlayerInput());
                    break;
                case ("radar", >= 3):
                    validChoiceMade = LoopWithMessage(PlaceRadar, GetPlayerInput, grids.player, "You can't place a radar there. Try somewhere else.");
                    break;
                case ("place mine", >= 1):
                    validChoiceMade = LoopWithMessage(PlaceMine, GetPlayerInput, grids.player, "You can't place a mine there. Try somewhere else.");
                    break;
                default:
                    Console.Write("\nInvalid input. Please try again.");
                    break;
            }
        }
    }



    static bool LoopWithMessage(Option option, Func<Point> obtainCoordinates, Node[,] grid, string message = "")
    {
        while (!option(grid, obtainCoordinates()))
        {
            Console.WriteLine(message);
        }

        return true;
    }    


    
    static Point GetPlayerInput()
    {
        while (true)
        {
            Console.Write("\nInput a letter (vertical coordinate), then a number (horizontal coordinate).");
            (char letter, int number) playerInput = (Console.ReadKey().KeyChar, int.Parse(Console.ReadKey().KeyChar.ToString()));
            
            // Checking if the letter is valid by using the character's values. See https://www.asciitable.com/ for more info.
            if (playerInput.letter >= 97 && playerInput.letter <= 104 && playerInput.number >= 1 && playerInput.number <= 8)
            {
                return new(playerInput.letter - 97, playerInput.number - 1);
            }
            else
            {
                Console.Write("\nInvalid coordinates entered. Please try agaim.");
            }
        } 
    }
}