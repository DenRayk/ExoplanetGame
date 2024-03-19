namespace ExoplanetGame
{
    internal class ExoplanetGame
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("ExoplanetGame starting...");

            GameServer gameServer = new();

            gameServer.Start();
        }
    }
}