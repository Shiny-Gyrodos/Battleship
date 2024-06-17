abstract class MainMenu // Potentially change from abstract in the future.
{
    // First ([0]) array inside jagged is for the words printed, second ([1]) is for not hovered variants, and the third ([2]) is for hovered variants.
    static string[][] textStorage = [["", "", ""], ["   Classic", "   Advanced", "   Chaos"], [" > Classic", " > Advanced", " > Chaos"]];
    public static void Start() // In need of refactoring
    {
        char playerInput = ' ';
        bool menuExited = false;
        int currentNumber = 0;

        while (!menuExited)
        {
            bool validCharInput = false;

            Console.WriteLine("Use the 'w' and 'a' keys to navigate up and down. Use the 'space' key to select a game mode.");

            for (int i = 0; i < textStorage.Length; i++)
            {
                if (currentNumber == i)
                {
                    textStorage[0][i] = textStorage[2][i]; // Jagged array solution hasn't been tested.
                }
                else
                {
                    textStorage[0][i] = textStorage[1][i]; // Jagged array solution hasn't been tested. 
                }
                
                Console.WriteLine(textStorage[0][i]); // Jagged array solution hasn't been tested.
            }

            while (!validCharInput)
            {
                playerInput = Console.ReadKey().KeyChar;
                validCharInput = playerInput == 'w' || playerInput == 's' || playerInput == ' ';
            }

            switch (playerInput)
            {
                case 'w':
                    currentNumber = currentNumber > 0 ? currentNumber - 1 : 0;
                    break;
                case 's':
                    currentNumber = currentNumber < textStorage.Length ? currentNumber + 1 : textStorage.Length;
                    break;
                case ' ':
                    menuExited = true;
                    break;
            }
        }

        switch (currentNumber) // Unfinished
        {
            case 0: // Selected classic
                break;
            case 1: // Selected advanced
                break;
            case 2: // Selected chaos
                break;
        }
    }
}