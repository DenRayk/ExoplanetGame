using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Menus.Controller;

namespace ExoplanetGame
{
    internal class ExoplanetGame
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("ExoplanetGame starting...");

            PlanetManager.ScanForExoplanets();

            GameServer gameServer = GameServer.GetInstance(ExoplanetMenuController.SelectTargetExoplanet());

            gameServer.Start();
        }
    }
}