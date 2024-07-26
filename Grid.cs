class Grid
{
    public Node[,] opponent = new Node[8, 8];
    public Node[,] player = new Node[8, 8];



    public Grid()
    {
        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                opponent[i, j] = new Node(false, false, 'O', 0);
                player[i, j] = new Node(false, false, 'O', 0);
            }
        }

        Ship.PlaceAll(opponent, false);
        Ship.PlaceAll(player, true);
    }
}



static class GridExtensions
{
    public static void Refresh(this Grid grid)
    {
        Console.Clear();
        Console.WriteLine("A B C D E F G H\n-----------------");

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                Console.Write($"{grid.opponent[i, j].Icon} ");
            }

            Console.Write($"| {i + 1}");
            Console.WriteLine();
        }

        Console.WriteLine("-----------------");

        for(int i = 0; i < 8; i++)
        {
            for(int j = 0; j < 8; j++)
            {
                Console.Write($"{grid.player[i, j].Icon} ");
            }
            Console.Write($"| {i + 1}");
            Console.WriteLine(); 
        }
    }
}