abstract class Grid
{
    public static Node[,] opponentGridHidden = new Node[8, 8];
    public static Node[,] opponentGrid = new Node[8, 8];
    public static Node[,] playerGrid = new Node[8, 8];

    public void Create(Node[,] chosenGrid)
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                chosenGrid[i, j] = new Node(false, false, ' ', "Empty");
            }
        }
    }



    public void Display(Node[,] chosenGrid)
    {
        Console.Clear();
        Console.WriteLine("0 1 2 3 4 5 6 7 8");

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                Console.Write($"{chosenGrid[i, j]} ");
            }
            Console.Write(i);
            Console.WriteLine();
        }
    }
}