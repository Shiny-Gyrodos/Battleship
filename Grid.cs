abstract class Grid
{
    public static NodeType[,] opponentGridHidden = new NodeType[8, 8];
    public static NodeType[,] opponentGrid = new NodeType[8, 8];
    public static NodeType[,] playerGrid = new NodeType[8, 8];

    public void Create(NodeType[,] chosenGrid)
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                chosenGrid[i, j] = new Empty{nodeFilled = false};
            }
        }
    }



    public void Display(NodeType[,] chosenGrid)
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