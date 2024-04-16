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
            Console.WriteLine($"Destinationpoint set to Exoplanet {PlanetManager.TargetPlanet.PlanetVariant}");
            Console.WriteLine("Select an option:");
            Console.WriteLine("1. Add Robot");
            Console.WriteLine("2. Select Robot");
            Console.WriteLine("3. Repair Robot");
            Console.WriteLine("4. Print map");
            Console.WriteLine("5. Exit");
        }

        public static int GetMainManueSelection()
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 5)
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

        public static void SelectRobotAndPartToRepair(GameServer gameServer, ControlCenter.ControlCenter controlCenter)
        {
            Console.WriteLine("Select a robot to repair:");
            controlCenter.DisplayRobots();

            int robotChoice;
            while (!int.TryParse(Console.ReadLine(), out robotChoice) || robotChoice < 1 ||
                   robotChoice > controlCenter.GetRobotCount())
            {
                Console.WriteLine("Invalid input. Please enter a valid robot number.");
            }

            Console.Clear();

            Console.WriteLine("Select a part to repair:");

            int counter = 1;
            foreach (string robotPart in Enum.GetNames(typeof(RobotPart)))
            {
                Console.WriteLine($"{counter}. {robotPart}");
                counter++;
            }

            int partChoice;
            while (!int.TryParse(Console.ReadLine(), out partChoice) || partChoice < 1 || partChoice > 4)
            {
                Console.WriteLine("Invalid input. Please enter a valid part number.");
            }

            Console.Clear();

            controlCenter.RepairRobotPart(robotChoice - 1, partChoice - 1);
        }
    }
}