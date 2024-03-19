using System.Runtime.CompilerServices;
using ExoplanetGame.Models;

namespace ExoplanetGame.Menus
{
    public class RobotMenu
    {
        public static void Show(RemoteRobot.RemoteRobot robot, ControlCenter.ControlCenter controlCenter)
        {
            bool keepMenuRunning = true;

            while (keepMenuRunning)
            {
                Console.WriteLine("Robot Menu");
                Console.WriteLine("1. Land");
                Console.WriteLine("2. Position");
                Console.WriteLine("3. Scan");
                Console.WriteLine("4. Move");
                Console.WriteLine("5. Rotate");
                Console.WriteLine("6. Crash");
                Console.WriteLine("7. Exit");

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 7)
                {
                    Console.WriteLine("Invalid input. Please enter a valid menu option.");
                }

                switch (choice)
                {
                    case 1:
                        robot.Land(SelectLandPosition());
                        break;

                    case 2:
                        Console.WriteLine($"Robot is at {robot.Position}");
                        break;

                    case 3:
                        Console.WriteLine("Scan selected.");
                        break;

                    case 4:
                        Console.WriteLine("Move selected.");
                        break;

                    case 5:
                        Console.WriteLine("Rotate selected.");
                        break;

                    case 6:
                        robot.Crash();
                        controlCenter.RemoveRobot(robot);
                        keepMenuRunning = false;
                        break;

                    case 7:
                        keepMenuRunning = false;
                        break;
                }
            }
        }

        private static Position SelectLandPosition()
        {
            Console.WriteLine("Enter the X coordinate:");
            int x;
            while (!int.TryParse(Console.ReadLine(), out x))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }

            Console.WriteLine("Enter the Y coordinate:");
            int y;
            while (!int.TryParse(Console.ReadLine(), out y))
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }

            return new Position(x, y);
        }
    }
}