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
                Console.WriteLine("5. Load");
                Console.WriteLine("6. Crash");
                Console.WriteLine("7. Back");
            }
            else
            {
                Console.WriteLine("1. Land");
                Console.WriteLine("2. Back");
            }

            Console.WriteLine("F1: Info");
        }

        public static void DisplayRobotMenuInformation(bool hasLanded)
        {
            if (hasLanded)
            {
                Console.WriteLine("Robot Menu Information");
                Console.WriteLine("Position: Show current position of the Robot");
                Console.WriteLine("Scan: Scan the environment");
                Console.WriteLine("Move: Move the robot in the direction it is facing");
                Console.WriteLine("Rotate: Rotate the robot left or right");
                Console.WriteLine("Load: Load energy to the robot");
                Console.WriteLine("Crash: Crash the robot");
            }
            else
            {
                Console.WriteLine("Robot Menu Information");
                Console.WriteLine("Land: Land the robot on the planet");
            }

            Console.WriteLine("Press ESC to go back");
        }



        public static int GetRobotMenuSelection(int minValue, int maxValue)
        {
            return MenuSelection.GetMenuSelection(minValue, maxValue);
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
                controlCenter.AddMeasure(robot.Scan(), robot.RobotInformation.Position);
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

        public static void LoadRobot(RobotBase robot)
        {
            Console.WriteLine("Enter the number of seconds to load energy:");
            int seconds = GetRobotMenuSelection(1, 30);
            robot.LoadEnergy(seconds);
        }

        public static void CrashRobot(RobotBase robot, ControlCenter.ControlCenter controlCenter, ref bool keepMenuRunning)
        {
            robot.Crash();
            controlCenter.RemoveRobot(robot);
            keepMenuRunning = false;
        }

        public static void HandleLandOption(RobotBase robot, ref bool hasLanded)
        {
            if (hasLanded)
            {
                Console.WriteLine("The robot has already landed.");
            }
            else
            {
                Position lanndPosition = robot.Land(SelectLandPosition());

                hasLanded = lanndPosition != null;
            }
        }

        public static void LoadCurrentExploredMap(ControlCenter.ControlCenter controlCenter)
        {
            PrintCurrentPlanetWeather();
            Console.Write($"Discovered area of the planet {PlanetManager.TargetPlanet.PlanetVariant}: ");
            controlCenter.PrintMap();
        }

        private static void PrintCurrentPlanetWeather()
        {
            Console.WriteLine($"Current weather on {PlanetManager.TargetPlanet.PlanetVariant}: {PlanetManager.TargetPlanet.Weather}");
        }

        public static Position SelectLandPosition()
        {
            Console.WriteLine("Enter the X coordinate:");
            int x = GetRobotMenuSelection(int.MinValue, int.MaxValue);

            Console.WriteLine("Enter the Y coordinate:");
            int y = GetRobotMenuSelection(int.MinValue, int.MaxValue);

            return new Position(x, y);
        }

        public static Rotation SelectRotation()
        {
            Console.WriteLine("Enter the rotation:");
            Console.WriteLine("1. Left");
            Console.WriteLine("2. Right");

            int rotation = GetRobotMenuSelection(1, 2);

            return rotation == 1 ? Rotation.LEFT : Rotation.RIGHT;
        }

    }
}