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
}