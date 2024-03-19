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
                        controlCenter.AddMeasure(robot.Scan(), robot.Position);
                        break;

                    case 4:
                        robot.Move();
                        break;

                    case 5:
                        robot.Rotate(SelectRotation());
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

        private static Rotation SelectRotation()
        {
            Console.WriteLine("Enter the rotation:");
            Console.WriteLine("1. Left");
            Console.WriteLine("2. Right");

            int rotation;
            while (!int.TryParse(Console.ReadLine(), out rotation) || rotation < 1 || rotation > 2)
            {
                Console.WriteLine("Invalid input. Please enter a valid rotation.");
            }

            return rotation == 1 ? Rotation.LEFT : Rotation.RIGHT;
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