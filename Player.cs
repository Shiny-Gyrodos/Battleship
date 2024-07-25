using System.Dynamic;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using MyApp;
class PlayerData
{
    int tokens;
    public int Tokens
    {
        get => tokens;
        set => tokens = value > 10 ? 10 : value;
    }
    public PlayerData()
    {
        Tokens = 0;
    }
}



class Player : Attacks
{
    delegate bool Option(Node[,] chosenGrid, Point point);
    public static void TakeTurn(Grid grids, PlayerData player)
    {
        invalidChoiceMade:

        Console.Write("\nChoose an option:\nShoot - 0 Cost | Strip Bomb - 8 Cost | Nuke - 8 Cost | Radar - 3 Cost | Mine - 1 Cost");
        string playerChoice = Console.ReadLine().ToLower();

        switch ((playerChoice, player.Tokens))
        {
            case ("shoot", _):
                LoopWithMessage(Shoot, GetPlayerInput, grids.opponent, "You can't shoot there. Try somewhere else.");
                player.Tokens++;
                break;
            case ("strip bomb", >= 8): // Succeeds no matter what
                StripBomb(grids.opponent, GetPlayerInputForStripBomb());
                break;
            case ("nuke", >= 8): // Succeeds no matter what
                Nuke(grids.opponent, GetPlayerInput());
                break;
            case ("radar", >= 3):
                LoopWithMessage(PlaceRadar, GetPlayerInput, grids.opponent, "You can't place a radar there. Try somewhere else.");
                break;
            case ("place mine", >= 1):
                LoopWithMessage(PlaceMine, GetPlayerInput, grids.opponent, "You can't place a mine there. Try somewhere else.");
                break;
            default:
                Console.WriteLine("\nInvalid input. Please try again.");
                goto invalidChoiceMade;
        }
    }



    static void LoopWithMessage(Option option, Func<Point> obtainCoordinates, Node[,] grid, string message)
    {
        while (!option(grid, obtainCoordinates()))
        {
            Console.WriteLine(message);
        }
    }    


    
    // Gets the player's desired coordinates for various actions.
    static Point GetPlayerInput()
    {
        while (true)
        {
            Console.Write("\nInput a letter (vertical coordinate), then a number (horizontal coordinate).");

            (char testLetter, char testNumber) = (Console.ReadKey().KeyChar, Console.ReadKey().KeyChar);       
            
            // Checking if the letter is valid by using the character's values. See https://www.asciitable.com/ for more info.
            if (ParseChar(testLetter) is var letter && letter.isColumn != null && ParseChar(testNumber) is var number && number.isColumn != null)
            {
                return new((int)letter.parsedValue, (int)number.parsedValue);
            }
            else
            {
                Console.Write("\nInvalid coordinates entered. Please try again.");
            }
        } 
    }



    // Gets the player's desired column or row to bomb.
    static (int number, bool isColumn) GetPlayerInputForStripBomb()
    {
        while (true)
        {
            Console.Write("\nInput the letter or number of the column or row you want to bomb.");

            char numOrLetter = Console.ReadKey().KeyChar; 

            if (ParseChar(numOrLetter) is var returnData && returnData.isColumn != null)
            {
                return ((int)returnData.parsedValue, (bool)returnData.isColumn);
            }
        }
    }



    static (int? parsedValue, bool? isColumn) ParseChar(char character)
    {
        if (character >= 49 && character <= 57)
        {
            return (character - 49, false);
        }
        else if (character >= 97 && character <= 104)
        {
            return (character - 97, true);
        }
        else
        {
            return (null, null);
        }
    }
}