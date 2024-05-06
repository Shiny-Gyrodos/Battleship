abstract class Grid
{
    public static NodeType[,] node = new NodeType[8, 8];
    public void Create()
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                node[i, j] = new Empty{nodeFilled = false};
            }
        }
    }



    public void Display()
    {
        Console.Clear();
        Console.WriteLine("0 1 2 3 4 5 6 7 8");

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                Console.Write($"{node[i, j]} ");
            }
            Console.Write(i);
            Console.WriteLine();
        }
    }
}