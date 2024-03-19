using System.Runtime.CompilerServices;
using ExoplanetGame.Models;

namespace ExoplanetGame.Menus
{
    public class RobotMenu
    {
        public static void Show(RemoteRobot.RemoteRobot robot, ControlCenter.ControlCenter controlCenter)
        {
            bool keepMenuRunning = true;
            bool hasLanded = robot.HasLanded();

            while (keepMenuRunning)
            {
                Console.WriteLine("Robot Menu");
                Console.WriteLine("1. Land");
                Console.WriteLine("2. Exit");

                // Display post-landing options only if the robot has landed
                if (hasLanded)
                {
                    Console.WriteLine("3. Position");
                    Console.WriteLine("4. Scan");
                    Console.WriteLine("5. Move");
                    Console.WriteLine("6. Rotate");
                    Console.WriteLine("7. Crash");
                }

                int choice;
                while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > 7)
                {
                    Console.WriteLine("Invalid input. Please enter a valid menu option.");
                }

                switch (choice)
                {
                    case 1:
                        if (!hasLanded)
                        {
                            robot.Land(SelectLandPosition());
                            hasLanded = true;
                        }
                        else
                        {
                            Console.WriteLine("The robot has already landed.");
                        }
                        break;

                    case 2:
                        keepMenuRunning = false;
                        break;

                    case 3:
                        if (hasLanded)
                        {
                            Console.WriteLine($"Robot is at {robot.Position}");
                        }
                        else
                        {
                            Console.WriteLine("The robot has not yet landed.");
                        }
                        break;

                    case 4:
                        if (hasLanded)
                        {
                            controlCenter.AddMeasure(robot.Scan(), robot.Position);
                        }
                        else
                        {
                            Console.WriteLine("The robot has not yet landed.");
                        }
                        break;

                    case 5:
                        if (hasLanded)
                        {
                            robot.Move();
                        }
                        else
                        {
                            Console.WriteLine("The robot has not yet landed.");
                        }
                        break;

                    case 6:
                        if (hasLanded)
                        {
                            robot.Rotate(SelectRotation());
                        }
                        else
                        {
                            Console.WriteLine("The robot has not yet landed.");
                        }
                        break;

                    case 7:
                        if (hasLanded)
                        {
                            robot.Crash();
                            controlCenter.RemoveRobot(robot);
                            keepMenuRunning = false;
                        }
                        else
                        {
                            Console.WriteLine("The robot has not yet landed.");
                        }
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