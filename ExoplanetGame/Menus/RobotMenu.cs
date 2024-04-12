using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Menus
{
    public class RobotMenu
    {
        public static void DisplayRobotMenuOptions(bool hasLanded)
        {
            Console.WriteLine(hasLanded ? "Planet research options:" : "Pre-Landing Options:");

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

        public static int GetRobotMenuSelection(int minValue, int maxValue)
        {
            int choice;

            while (!int.TryParse(Console.ReadLine(), out choice) || choice < minValue || choice > maxValue)
            {
                Console.WriteLine($"Invalid input. Please enter a number between {minValue} and {maxValue}.");
            }

            Console.Clear();

            return choice;
        }

        public static void HandleLandOption(RobotBase robot, ref bool hasLanded)
        {
            if (hasLanded)
            {
                Console.WriteLine("The robot has already landed.");
                
            }
            else
            {
                hasLanded = robot.Land(SelectLandPosition());
            }
        }

        public static void ShowCurrentPosition(RobotBase robot)
        {
            Console.WriteLine($"Robot is at {robot.GetPosition()}");
        }

        public static void ScanEnvironment(RobotBase robot, ControlCenter.ControlCenter controlCenter)
        {
            if (robot.RobotVariant == RobotVariant.SCOUT)
            {
                if (robot is ScoutBot scoutBot)
                {
                    Dictionary<Measure, Position> measures = scoutBot.ScoutScan();

                    controlCenter.AddMeasures(measures);
                }
            }
            else
            {
                controlCenter.AddMeasure(robot.Scan(), robot.RobotStatus.Position);
            }
        }

        public static bool MoveRobot(RobotBase robot)
        {
            if (robot.Move() == null)
            {
                return false;
            }
            return true;
        }

        public static void RotateRobot(RobotBase robot)
        {
            robot.Rotate(SelectRotation());
        }

        public static void CrashRobot(RobotBase robot, ControlCenter.ControlCenter controlCenter, ref bool keepMenuRunning)
        {
            robot.Crash();
            controlCenter.RemoveRobot(robot);
            keepMenuRunning = false;
        }

        public static Rotation SelectRotation()
        {
            Console.WriteLine("Enter the rotation:");
            Console.WriteLine("1. Left");
            Console.WriteLine("2. Right");

            int rotation = GetRobotMenuSelection(1, 2);

            return rotation == 1 ? Rotation.LEFT : Rotation.RIGHT;
        }

        public static Position SelectLandPosition()
        {
            Console.WriteLine("Enter the X coordinate:");
            int x = GetRobotMenuSelection(int.MinValue, int.MaxValue);

            Console.WriteLine("Enter the Y coordinate:");
            int y = GetRobotMenuSelection(int.MinValue, int.MaxValue);

            return new Position(x, y);
        }

        public static void LoadCurrentExploredMap(ControlCenter.ControlCenter controlCenter)
        {
            Console.WriteLine("Discovered area of the planet:");
            controlCenter.PrintMap();
        }
    }
}