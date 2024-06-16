abstract class MainMenu // Potentially change from abstract in the future.
{
    static string[] textStorage = ["Classic", "Advanced", "Chaos"];

    public static void Start() // In need of refactoring
    {
        char playerInput = ' ';
        bool menuExited = false;
        int currentNumber = 0;

        while (!menuExited)
        {
            bool validCharInput = false;

            for (int i = 0; i < textStorage.Length; i++)
            {
                if (currentNumber == i)
                {
                    textStorage[i] = textStorage[i].Insert(0, " > ");
                }
                else
                {
                    textStorage[i] = textStorage[i].Insert(0, "   ");
                }
                
                Console.WriteLine(textStorage[i]);
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