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



    public void Display() // Needs some fine tuning.
    {
        Console.Clear();
        Console.WriteLine("0 1 2 3 4 5 6 7\n-----------------");

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                Console.Write($"{opponentGrid[i, j].Icon} ");
            }
            Console.Write("| " + i);
            Console.WriteLine();
        }

        Console.WriteLine("-----------------");

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                Console.Write($"{playerGrid[i, j].Icon} ");
            }
            Console.Write("| " + i);
            Console.WriteLine();
        }
    }
}