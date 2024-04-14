using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet
{
    public class ExoplanetBase
    {
        public Topography Topography { get; set; }

        protected RobotManager robotManager;
        public PlanetVariant PlanetVariant;

        public ExoplanetBase()
        {
            robotManager = new RobotManager();
        }

        public virtual int GetRobotCount()
        {
            return robotManager.GetRobotCount();
        }

        public virtual void RemoveRobot(RobotBase Robot)
        {
            robotManager.RemoveRobot(Robot);
        }

        public virtual bool Land(RobotBase robot, Position landPosition)
        {
            return robotManager.LandRobot(robot, landPosition, Topography);
        }

        public virtual Position Move(RobotBase Robot)
        {
            return robotManager.MoveRobot(Robot, Topography);
        }

        public virtual Direction Rotate(RobotBase robot, Rotation rotation)
        {
            return robotManager.RotateRobot(robot, rotation);
        }

        public virtual Measure Scan(RobotBase robot)
        {
            return robotManager.Scan(robot, Topography);
        }

        public virtual Dictionary<Measure, Position> ScoutScan(RobotBase robot)
        {
            return robotManager.ScoutScan(robot, Topography);
        }

        public virtual void Remove(RobotBase robot)
        {
            Console.WriteLine($"Remove: {robot.GetLanderName()}");
            robotManager.RemoveRobot(robot);
        }

        public virtual Position GetRobotPosition(RobotBase robot)
        {
            return robotManager.GetRobotPosition(robot);
        }

        public bool LandLavaBot(LavaBot lavaBot, Position landPosition)
        {
            return robotManager.LandLavaBot(lavaBot, landPosition, Topography);
        }

        public Position MoveLavaBot(LavaBot lavaBot)
        {
            return robotManager.MoveLavaBot(lavaBot, Topography);
        }
    }
}