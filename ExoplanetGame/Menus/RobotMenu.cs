using ExoplanetGame.Robot;

namespace ExoplanetGame.Menus
{
    public class RobotMenu
    {
        public static void Show(RobotBase robot, ControlCenter.ControlCenter controlCenter)
        {
            bool keepMenuRunning = true;
            bool hasLanded = robot.HasLanded();

            while (keepMenuRunning)
            {
                Console.WriteLine("Robot Menu");
                DisplayMenuOptions(hasLanded);

                int choice = GetUserChoice(1, hasLanded ? 8 : 2);

                switch (choice)
                {
                    case 1:
                        if (hasLanded)
                        {
                            ShowCurrentPosition(robot);
                        }
                        else
                        {
                            HandleLandOption(robot, ref hasLanded);
                        }
                        break;

                    case 2:
                        if (hasLanded)
                        {
                            ScanEnvironment(robot, controlCenter);
                        }
                        else
                        {
                            keepMenuRunning = false;
                        }
                        break;

                    case 3:
                        if (hasLanded)
                            keepMenuRunning = MoveRobot(robot);
                        break;

                    case 4:
                        if (hasLanded)
                            RotateRobot(robot);
                        break;

                    case 5:
                        if (hasLanded)
                            CrashRobot(robot, controlCenter, ref keepMenuRunning);
                        else
                            keepMenuRunning = false;
                        break;

                    case 6:
                        if (hasLanded)
                        {
                            keepMenuRunning = false;
                        }
                        break;
                }
            }
        }

        private static void DisplayMenuOptions(bool hasLanded)
        {
            Console.WriteLine(hasLanded ? "Post-Landing Options:" : "Pre-Landing Options:");

            if (hasLanded)
            {
                Console.WriteLine("1. Position");
                Console.WriteLine("2. Scan");
                Console.WriteLine("3. Move");
                Console.WriteLine("4. Rotate");
                Console.WriteLine("5. Crash");
                Console.WriteLine("6. Exit");
            }
            else
            {
                Console.WriteLine("1. Land");
                Console.WriteLine("2. Exit");
            }
        }

        private static int GetUserChoice(int minValue, int maxValue)
        {
            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < minValue || choice > maxValue)
            {
                Console.WriteLine($"Invalid input. Please enter a number between {minValue} and {maxValue}.");
            }
            return choice;
        }

        private static void HandleLandOption(RobotBase robot, ref bool hasLanded)
        {
            if (!hasLanded)
            {
                robot.Land(SelectLandPosition());
                hasLanded = true;
            }
            else
            {
                Console.WriteLine("The robot has already landed.");
            }
        }

        private static void ShowCurrentPosition(RobotBase robot)
        {
            Console.WriteLine($"Robot is at {robot.RobotStatus.Position}");
        }

        private static void ScanEnvironment(RobotBase robot, ControlCenter.ControlCenter controlCenter)
        {
            controlCenter.AddMeasure(robot.Scan(), robot.RobotStatus.Position);
        }

        private static bool MoveRobot(RobotBase robot)
        {
            if (robot.Move() == null)
            {
                return false;
            }
            return true;
        }

        private static void RotateRobot(RobotBase robot)
        {
            robot.Rotate(SelectRotation());
        }

        private static void CrashRobot(RobotBase robot, ControlCenter.ControlCenter controlCenter, ref bool keepMenuRunning)
        {
            robot.Crash();
            controlCenter.RemoveRobot(robot);
            keepMenuRunning = false;
        }

        private static Rotation SelectRotation()
        {
            Console.WriteLine("Enter the rotation:");
            Console.WriteLine("1. Left");
            Console.WriteLine("2. Right");

            int rotation = GetUserChoice(1, 2);

            return rotation == 1 ? Rotation.LEFT : Rotation.RIGHT;
        }

        private static Position SelectLandPosition()
        {
            Console.WriteLine("Enter the X coordinate:");
            int x = GetUserChoice(int.MinValue, int.MaxValue);

            Console.WriteLine("Enter the Y coordinate:");
            int y = GetUserChoice(int.MinValue, int.MaxValue);

            return new Position(x, y);
        }
    }
}