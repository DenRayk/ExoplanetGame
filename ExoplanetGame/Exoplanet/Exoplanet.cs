using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Exoplanet
{
    public class Exoplanet
    {
        public Topography Topography { get; set; }
        private RobotManager robotManager;

        public Exoplanet()
        {
            Topography = new Topography([
                "GSSWPFSGGL",
                "SPW4PSFFLL",
                "SGWPMSFLLF",
                "SSWWMGSFFG",
                "FGSWWSSGFG",
                "FFWWGGSGFF"
            ]);
            robotManager = new RobotManager();
        }

        public int GetRobotCount()
        {
            return robotManager.GetRobotCount();
        }

        public void RemoveRobot(DefaultRobot Robot)
        {
            robotManager.RemoveRobot(Robot);
        }

        public bool Land(DefaultRobot Robot, Position landPosition)
        {
            return robotManager.LandRobot(Robot, landPosition, Topography);
        }

        public Position Move(DefaultRobot Robot)
        {
            return robotManager.MoveRobot(Robot, Topography);
        }

        public Direction Rotate(DefaultRobot robot, Rotation rotation)
        {
            return robotManager.RotateRobot(robot, rotation);
        }

        public Measure Scan(DefaultRobot robot)
        {
            return Topography.GetMeasureAtPosition(robotManager.GetRobotPosition(robot));
        }

        public void Remove(DefaultRobot robot)
        {
            Console.WriteLine($"Remove: {robot.GetLanderName()}");
            robotManager.RemoveRobot(robot);
        }
    }
}