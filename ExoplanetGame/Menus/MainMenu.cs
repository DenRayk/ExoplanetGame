using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Menus
{
    internal class MainMenu
    {
        public static void DisplayMainMenuOptions()
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine($"Destinationpoint set to Exoplanet {PlanetManager.TargetPlanet.PlanetVariant}");
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Add Robot");
            Console.WriteLine("2. Select Robot");
            Console.WriteLine("3. Print map");
            Console.WriteLine("4. Exit");
            Console.WriteLine("F1. Info");
        }

        public static void DisplayMainMenuInformation()
        {
            Console.WriteLine("Main Menu Information");
            Console.WriteLine("Add Robot: Add a robot to the current layout");
            Console.WriteLine("Select Robot: Select a robot to be sent from the layout to the exoplanet");
            Console.WriteLine("Print map: Display status of the exoplanet's exploration area");
            Console.WriteLine("Press ESC to go back");
        }

        public static int GetMainMenuSelection(int minValue, int maxValue)
        {
            return MenuSelection.GetMenuSelection(minValue, maxValue);
        }
    }
}