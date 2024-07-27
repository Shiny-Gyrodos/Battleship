static class Game
{
    public static void Start()
    {
        bool gameActive = true;
        Grid grids = new();
        Brain brain = new(grids.player);
        PlayerData playerData = new(grids.opponent);

        while (gameActive)
        {
            grids.Refresh();
            gameActive = Player.TakeTurn(ref grids, ref playerData);
            grids.Refresh();

            if (gameActive)
            {
                gameActive = Opponent.TakeTurn(ref grids, ref brain);
                Thread.Sleep(1500);
                grids.Refresh();    
            }      
        }
    }
}