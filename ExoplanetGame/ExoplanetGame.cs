using ExoplanetGame.Application;
using ExoplanetGame.Application.ControlCenter;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Menus.Controller;
using ExoplanetGame.Presentation.Commands.PlanetSelection;

namespace ExoplanetGame
{
    internal class ExoplanetGame
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("ExoplanetGame starting...");

            UCCollection ucCollection = new();
            ExoplanetService exoplanetService = new();

            ucCollection.init(exoplanetService);

            ShowPlanetSelectionCommand showPlanetSelectionCommand = new(ucCollection, exoplanetService);
            showPlanetSelectionCommand.Execute();
        }
    }
}