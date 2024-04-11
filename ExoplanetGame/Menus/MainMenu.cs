using ExoplanetGame.Robot;

namespace ExoplanetGame.Menus
{
    internal class MainMenu
    {
        public static void Show(GameServer gameServer, ControlCenter.ControlCenter controlCenter)
        {
            while (true)
            {
                Console.WriteLine("Main Menu");
                Console.WriteLine("1. Add Robot");
                Console.WriteLine("2. Select Robot");
                Console.WriteLine("3. Print map");
                Console.WriteLine("4. Exit");

                var choice = getChoice();

                switch (choice)
                {
                    case 1:
                        gameServer.AddRobot(SelectRobotVariant());
                        break;
                    case 2:
                        if (controlCenter.GetRobotCount() == 0)
                        {
                            Console.WriteLine("No robots to control.");
                            break;
                        }
                        SelectRobot(gameServer, controlCenter);
                        break;

                    case 3:
                        controlCenter.PrintMap();
                        break;

                    case 4:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        private static int getChoice()
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 4)
            {
                Console.WriteLine("Invalid input. Please enter a valid menu option.");
            }

            return choice;
        }

        public static RobotVariant SelectRobotVariant()
        {
            Console.WriteLine("Select a robot variant:");
            Console.WriteLine("1. Default robot");
            Console.WriteLine("2. Scout robot");
            Console.WriteLine("3. Lava robot");
            Console.WriteLine("4. Aqua robot");
            Console.WriteLine("5. Mud robot");
            Console.WriteLine("6. Solar robot");

            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1: 
                    return RobotVariant.DEFAULT;
                case 2: 
                    return RobotVariant.SCOUT;
                case 3:
                    return RobotVariant.LAVA;
                case 4:
                    return RobotVariant.AQUA;
                case 5:
                    return RobotVariant.MUD;
                case 6:
                    return RobotVariant.SOLAR;
                default: 
                    throw new ArgumentException("Invalid choice. Please select a valid robot variant.");
            }
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

            gameServer.ControlRobot(choice - 1);
        }
    }
}