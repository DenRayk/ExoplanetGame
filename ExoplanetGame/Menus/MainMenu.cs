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
            Console.WriteLine($"Research Exoplanet {PlanetManager.TargetPlanet.PlanetVariant}");
            Console.WriteLine("Select an option (press F1 for help):\n");
            Console.WriteLine("1. Add Robot");
            Console.WriteLine("2. Select Robot");
            Console.WriteLine("3. Repair Robot");
            Console.WriteLine("4. Print map");
            Console.WriteLine("5. Exit");
        }

        public static void DisplayMainMenuInformation()
        {
            Console.WriteLine("Main Menu Information\n");
            Console.WriteLine("Add Robot:\t Add a robot to the current layout");
            Console.WriteLine("Select Robot:\t Select a robot to be sent from the layout to the exoplanet");
            Console.WriteLine("Repair Robot:\t Repair a robot's part");
            Console.WriteLine("Print map:\t Display status of the exoplanet's exploration area\n");
            Console.WriteLine("Press ESC to go back");
        }

        public static int GetMainMenuSelection(int minValue, int maxValue)
        {
            return MenuSelection.GetMenuSelection(minValue, maxValue);
        }

        public static void SelectRobotAndPartToRepair(GameServer gameServer, ControlCenter.ControlCenter controlCenter)
        {
            Console.WriteLine("Select a robot to repair:");
            controlCenter.DisplayRobots();

            int robotChoice = MenuSelection.GetMenuSelection(1, controlCenter.GetRobotCount());

            Console.WriteLine("Select a part to repair:");

            int counter = 1;
            foreach (string robotPart in Enum.GetNames(typeof(RobotPart)))
            {
                Console.WriteLine($"{counter}. {robotPart}");
                counter++;
            }

            int partChoice = MenuSelection.GetMenuSelection(1, 4);

            controlCenter.RepairRobotPart(robotChoice - 1, partChoice - 1);
        }
    }
}