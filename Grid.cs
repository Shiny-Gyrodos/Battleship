class Grid
{
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
        Console.WriteLine("A B C D E F G H\n-----------------");

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                Console.Write($"{opponentGrid[i, j].Icon} ");
            }

            Console.Write($"| {i + 1}");
            Console.WriteLine();
        }

        Console.WriteLine("-----------------");

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                Console.Write($"{playerGrid[i, j].Icon} ");
            }
            Console.Write($"| {i + 1}");
            Console.WriteLine(); 
        }
    }
}