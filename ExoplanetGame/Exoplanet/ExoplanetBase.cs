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
            robotManager = new RobotManager(Topography);
        }

        public virtual int GetRobotCount()
        {
            return robotManager.GetRobotCount();
        }

        public virtual void RemoveRobot(RobotBase Robot)
        {
            robotManager.RemoveRobot(Robot);
        }

        public virtual Position Land(RobotBase robot, Position landPosition)
        {
            return robotManager.LandController.LandRobot(robot, landPosition, Topography);
        }

        public virtual Position Move(RobotBase Robot)
        {
            return robotManager.MoveController.MoveRobot(Robot, Topography);
        }

        public virtual Direction Rotate(RobotBase robot, Rotation rotation)
        {
            return robotManager.RotateRobot(robot, rotation);
        }

        public virtual Measure Scan(RobotBase robot)
        {
            return robotManager.ScanController.Scan(robot, Topography);
        }

        public virtual Dictionary<Measure, Position> ScoutScan(RobotBase robot)
        {
            return robotManager.ScanController.ScoutScan(robot, Topography);
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

        public Position LandLavaBot(LavaBot lavaBot, Position landPosition)
        {
            return robotManager.LandController.LandLavaBot(lavaBot, landPosition, Topography);
        }

        public Position MoveLavaBot(LavaBot lavaBot)
        {
            return robotManager.MoveController.MoveLavaBot(lavaBot, Topography);
        }

        public Position MoveMudBot(MudBot mudBot)
        {
            return robotManager.MoveController.MoveMudBot(mudBot, Topography);
        }

        public Position LandAquaBot(AquaBot aquaBot, Position landPosition)
        {
            return robotManager.LandController.LandAquaBot(aquaBot, landPosition, Topography);
        }

        public Position MoveAquaBot(AquaBot aquaBot)
        {
            return robotManager.MoveController.MoveAquaBot(aquaBot, Topography);
        }
    }
}