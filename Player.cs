class Player : Attacks
{
    static int playerTokens = 0;
    static void ChooseFiringMode(Grid grids)
    {
        bool validChoiceMade = false;

        while (!validChoiceMade)
        {
            Console.Write("\nChoose a firing mode:\nNormal Attack - 0 Cost | Strip Bomb - 8 Cost | Nuke - 8 Cost | Radar - 3 Cost | Place Mine - 1 Cost");
            string playerChoice = Console.ReadLine().ToLower();

            switch (playerChoice)
            {
                case "normal attack": // Repeates till succeeds
                    validChoiceMade = true;
                    while (!Shoot(grids.opponentGrid, GetPlayerInput()))
                    {
                        Console.Write("You've already shot there. Try somewhere else.");
                    }
                    break;
                case "strip bomb": // Succeeds no matter what
                    validChoiceMade = true;
                    //StripBomb();
                    break;
                case "nuke": // Succeeds no matter what
                    validChoiceMade = true;
                    Nuke(grids.opponentGrid, GetPlayerInput());
                    break;
                case "radar": // Repeates till succeeds
                    validChoiceMade = true;
                    while (!PlaceRadar(grids.opponentGrid, GetPlayerInput()))
                    {
                        Console.Write("You can't place a radar there. Try somewhere else.");
                    }
                    break;
                case "place mine": // Repeates till succeeds
                    validChoiceMade = true;
                    while (!PlaceMine(grids.opponentGrid, GetPlayerInput()))
                    {
                        Console.Write("You can't place a mine there. Try somewhere else.");
                    }
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