class Player : Attacks
{
    static int playerTokens = 0;
    static void TakeTurn(Grid grids)
    {
        bool validChoiceMade = false;

        while (!validChoiceMade)
        {
            Console.Write("\nChoose a firing mode:\nShoot - 0 Cost | Strip Bomb - 8 Cost | Nuke - 8 Cost | Radar - 3 Cost | Place Mine - 1 Cost");
            string playerChoice = Console.ReadLine().ToLower();

            switch ((playerChoice, playerTokens))
            {
                case ("shoot", _):
                    validChoiceMade = LoopUntilExecution(PlaceRadar, GetPlayerInput, grids.playerGrid, "You can't shoot there. Try somewhere else.");
                    playerTokens++;
                    break;
                case ("strip bomb", >= 8): // Succeeds no matter what
                    validChoiceMade = true;
                    //StripBomb();
                    break;
                case ("nuke", >= 8): // Succeeds no matter what
                    validChoiceMade = true;
                    Nuke(grids.opponentGrid, GetPlayerInput());
                    break;
                case ("radar", >= 3):
                    validChoiceMade = LoopUntilExecution(PlaceRadar, GetPlayerInput, grids.playerGrid, "You can't place a radar there. Try somewhere else.");
                    break;
                case ("place mine", >= 1):
                    validChoiceMade = LoopUntilExecution(PlaceMine, GetPlayerInput, grids.playerGrid, "You can't place a mine there. Try somewhere else.");
                    break;
                default:
                    Console.Write("\nInvalid input. Please try again.");
                    break;
            }
        }
    }


    
    static (int, int) GetPlayerInput()
    {
        while (true)
        {
            Console.Write("\nInput a letter (vertical coordinate), then a number (horizontal coordinate).");
            (char letter, int number) playerInput = (Console.ReadKey().KeyChar, int.Parse(Console.ReadKey().KeyChar.ToString()));
            
            // Checking if the letter is valid by using the character's values. See https://www.asciitable.com/ for more info.
            if (playerInput.letter >= 97 && playerInput.letter <= 104 && playerInput.number >= 1 && playerInput.number <= 8)
            {
                return (playerInput.letter - 97, playerInput.number - 1);
            }
            else
            {
                Console.Write("\nInvalid coordinates entered. Please try agaim.");
            }
        } 
    }
}