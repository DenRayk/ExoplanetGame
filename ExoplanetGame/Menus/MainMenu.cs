using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Menus
{
    internal class MainMenu
    {
        public static void DisplayMainManueOptions()
        {
            Console.WriteLine("Main Menu");
            Console.WriteLine($"Destinationpoint set to Exoplanet {PlanetManager.TargetPlanet.GetPlanetVariant()}");
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Add Robot");
            Console.WriteLine("2. Select Robot");
            Console.WriteLine("3. Print map");
            Console.WriteLine("4. Exit");
        }

        public static int GetMainManueSelection()
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
            {
                Console.WriteLine("Invalid input. Please enter a valid menu option.");
            }

            Console.Clear();

            return choice;
        }

        public static void SelectRobot(GameServer gameServer, ControlCenter.ControlCenter controlCenter)
        {
            Console.WriteLine("Select a robot to control:");
            controlCenter.DisplayRobots();

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > controlCenter.GetRobotCount())
            {
                Console.WriteLine("Invalid input. Please enter a valid robot number.");
            }
            
            Console.Clear();

            gameServer.ControlRobot(choice - 1);
        }
    }
}