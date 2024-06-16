class Grid
{
    public Node[,] opponentGridHidden = new Node[8, 8];
    public Node[,] opponentGrid = new Node[8, 8];
    public Node[,] playerGrid = new Node[8, 8];



    public void Create(Node[,] chosenGrid)
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                chosenGrid[i, j] = new Node(false, false, 'O', 0);
            }
        }
    }



    public void Display()
    {
        Console.Clear();
        Console.WriteLine("1 2 3 4 5 6 7 8\n-----------------");

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                Console.Write($"{opponentGrid[i, j].Icon} ");
            }

            Console.Write($"| {Convert.ToChar(i + 65)}");
            Console.WriteLine();
        }

        Console.WriteLine("-----------------");

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                Console.Write($"{playerGrid[i, j].Icon} ");
            }
            Console.Write($"| {Convert.ToChar(i + 65)}"); // Changes the character by adding a value to it. E. https://www.asciitable.com
            Console.WriteLine(); 
        }
    }
}