using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.RobotResults;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Menus
{
    public class RobotMenu
    {
        public static void DisplayRobotMenuInformation(bool hasLanded)
        {
            if (hasLanded)
            {
                Console.WriteLine("Robot Menu Information\n");
                Console.WriteLine("Position:\t\t Show current position of the Robot");
                Console.WriteLine("Scan:\t\t Scan the environment");
                Console.WriteLine("Move:\t\t Move the robot in the direction it is facing");
                Console.WriteLine("Rotate:\t\t Rotate the robot left or right");
                Console.WriteLine("Load:\t\t Load energy to the robot");
                Console.WriteLine("Crash:\t\t Crash the robot\n");
            }
            else
            {
                Console.WriteLine("Robot Menu Information\n");
                Console.WriteLine("Land:\t Land the robot on the planet\n");
            }

            Console.WriteLine("Press ESC to go back");
        }

        public static void DisplayRobotMenuOptions(bool hasLanded)
        {
            Console.WriteLine(hasLanded ? "Planet research options (press F1 for help):" : "Pre-Landing Options (press F1 for help):");

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
        }

        public static void CrashRobot(RobotBase robot, ControlCenter.ControlCenter controlCenter, ref bool keepMenuRunning)
        {
            robot.Crash();
            controlCenter.RemoveRobot(robot);
            keepMenuRunning = false;
        }

        public static int GetRobotMenuSelection(int minValue, int maxValue)
        {
            return MenuSelection.GetMenuSelection(minValue, maxValue, true);
        }

        public static void HandleLandOption(RobotBase robot, ref bool hasLanded)
        {
            if (hasLanded)
            {
                Console.WriteLine("The robot has already landed.");
            }
            else
            {
                PositionResult landResult = robot.Land(SelectLandPosition());

                hasLanded = landResult.IsSuccess;
            }
        }

        public static void LoadCurrentExploredMap(ControlCenter.ControlCenter controlCenter)
        {
            PrintCurrentPlanetWeather();
            Console.Write($"Discovered area of the planet {PlanetManager.TargetPlanet.PlanetVariant}: ");
            controlCenter.PrintMap();
        }

        public static void LoadRobot(RobotBase robot)
        {
            Console.WriteLine("Enter the number of seconds to load energy:");
            int seconds = GetRobotMenuSelection(1, 30);
            robot.LoadEnergy(seconds);
        }

        public static bool MoveRobot(RobotBase robot)
        {
            return robot.Move().HasRobotSurvived;
        }

        public static void RotateRobot(RobotBase robot)
        {
            robot.Rotate(SelectRotation());
        }

        public static void ScanEnvironment(RobotBase robot, ControlCenter.ControlCenter controlCenter)
        {
            if (robot.RobotVariant == RobotVariant.SCOUT)
            {
                if (robot is ScoutBot scoutBot)
                {
                    ScoutScanResult scoutScanResult = scoutBot.ScoutScan();

                    controlCenter.AddMeasures(scoutScanResult.Measures);
                }
            }
            else
            {
                ScanResult scanResult = robot.Scan();

                if (scanResult.IsSuccess)
                {
                    controlCenter.AddMeasure(scanResult.Measure, robot.RobotInformation.Position);
                }
            }
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

        public static void ShowCurrentPosition(RobotBase robot)
        {
            Console.WriteLine($"Robot is at {robot.GetPosition().Position}");
        }

        private static void PrintCurrentPlanetWeather()
        {
            Console.WriteLine($"Current weather on {PlanetManager.TargetPlanet.PlanetVariant}: {PlanetManager.TargetPlanet.Weather}");
        }
    }
}