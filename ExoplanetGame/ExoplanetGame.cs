using ExoplanetGame.ControlCenter;

namespace ExoplanetGame
{
    internal class ExoplanetGame
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Exoplanet rising up...");

            Exoplanet.Exoplanet exoPlanet = new();

            Console.WriteLine("ControlCenter rising up...");

            ControlServer controlServer = new(exoPlanet);

            controlServer.Start();
        }
    }
}