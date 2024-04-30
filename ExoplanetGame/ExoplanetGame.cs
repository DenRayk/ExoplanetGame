using ExoplanetGame.Application;
using ExoplanetGame.Application.ControlCenter;
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

            ShowPlanetSelectionCommand showPlanetSelectionCommand = new(ucCollection);
            showPlanetSelectionCommand.Execute();
        }
    }
}