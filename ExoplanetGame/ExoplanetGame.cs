using ExoplanetGame.ControlCenter;
using ExoplanetGame.Menus.Controller;
using ExoplanetGame.Presentation.Commands.MainMenu;
using ExoplanetGame.Presentation.Commands.PlanetSelection;

namespace ExoplanetGame
{
    internal class ExoplanetGame
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("ExoplanetGame starting...");

            ShowPlanetSelectionCommand showPlanetSelectionCommand = new();
            showPlanetSelectionCommand.Execute();
        }
    }
}