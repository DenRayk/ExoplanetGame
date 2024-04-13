using ExoplanetGame.ControlCenter;
using ExoplanetGame.Menus;

namespace ExoplanetGame
{
    internal class ExoplanetGame
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("ExoplanetGame starting...");

            PlanetManager.ScanForExoplanets();

            GameServer gameServer = new GameServer(ExoplanetMenu.SelectTargetExoplanet());

            gameServer.Start();
        }
    }
}