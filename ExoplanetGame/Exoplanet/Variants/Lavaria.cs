using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Lavaria : IExoplanet
    {
        public Topography Topography { get; }
        private RobotManager robotManager;

        public Lavaria()
        {
            Topography = new Topography(new string[]
            {
                "GSSWPFSGGL",
                "SPW4PSFFLL",
                "SGWPMSFLLF",
                "SSWWMGSFFG",
                "FGSWWSSGFG",
                "FFWWGGSGFF"
            });
            robotManager = new RobotManager();
        }

        public int GetRobotCount()
        {
            return robotManager.GetRobotCount();
        }

        public void RemoveRobot(RobotBase Robot)
        {
            robotManager.RemoveRobot(Robot);
        }

        public bool Land(RobotBase Robot, Position landPosition)
        {
            return robotManager.LandRobot(Robot, landPosition, Topography);
        }

        public Position Move(RobotBase Robot)
        {
            return robotManager.MoveRobot(Robot, Topography);
        }

        public Direction Rotate(RobotBase robot, Rotation rotation)
        {
            return robotManager.RotateRobot(robot, rotation);
        }

        public Measure Scan(RobotBase robot)
        {
            return robotManager.Scan(robot, Topography);
        }

        public Dictionary<Measure, Position> ScoutScan(RobotBase robot)
        {
            return robotManager.ScoutScan(robot, Topography);
        }

        public Position GetRobotPosition(RobotBase robot)
        {
            return robotManager.GetRobotPosition(robot);
        }

        public void Remove(RobotBase robot)
        {
            Console.WriteLine($"Remove: {robot.GetLanderName()}");
            robotManager.RemoveRobot(robot);
        }
    }
}