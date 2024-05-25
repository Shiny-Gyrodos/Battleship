class MainMenu
{
    static string[] textStorage = ["Classic", "Advanced", "Chaos"];

    public void Enter() // In need of refactoring
    {
        char playerInput = ' ';
        bool menuLeft = false;
        int currentNumber = 0;

        while (!menuLeft)
        {
            bool validCharInput = false;

            for (int i = 0; i < textStorage.Length; i++)
            {
                if (currentNumber == i)
                {
                    textStorage[i].Insert(0, " > ");
                    Console.WriteLine(textStorage[i]);
                }
                else
                {
                    textStorage[i].Insert(0, "   ");
                    Console.WriteLine(textStorage[i]);
                }
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
                    menuLeft = true;
                    break;
            }
        }

        switch (currentNumber) // Unfinished
        {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
        }
    }
}