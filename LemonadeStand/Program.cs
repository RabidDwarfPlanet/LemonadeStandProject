namespace LemonadeStand
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Game game = new Game();
            
            game.WelcomeMessage();
            game.PlayerCount();
            game.NamePlayers();
            if (game.playerCount == 1) { game.SoloDayCycle(); }
            else { game.MultiDayCycle(); }
            game.EndGame();
            
        }
    }
}