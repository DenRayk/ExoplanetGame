using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.RobotResults;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet
{
    public class ExoplanetBase
    {
        internal Weather Weather { get; set; }
        public Topography Topography { get; set; }

        protected RobotManager robotManager;
        public PlanetVariant PlanetVariant { get; }

        public ExoplanetBase(PlanetVariant planetVariant)
        {
            PlanetVariant = planetVariant;
            Weather = Weather.SUNNY;
            robotManager = new RobotManager(this);
        }

        public virtual int GetRobotCount()
        {
            return robotManager.GetRobotCount();
        }

        public virtual void RemoveRobot(RobotBase Robot)
        {
            robotManager.RemoveRobot(Robot);
        }

        public virtual PositionResult Land(RobotBase robot, Position landPosition)
        {
            return robotManager.LandController.LandRobot(robot, landPosition, Topography);
        }

        public virtual PositionResult Move(RobotBase robot)
        {
            ChangeWeather();
            return robotManager.MoveController.MoveRobot(robot, Topography);
        }

        public virtual RotationResult Rotate(RobotBase robot, Rotation rotation)
        {
            ChangeWeather();
            return robotManager.RotateRobot(robot, rotation);
        }

        public virtual ScanResult Scan(RobotBase robot)
        {
            ChangeWeather();
            return robotManager.ScanController.Scan(robot, Topography);
        }

        public virtual ScoutScanResult ScoutScan(RobotBase robot)
        {
            ChangeWeather();
            return robotManager.ScanController.ScoutScan(robot, Topography);
        }

        public virtual Position GetRobotPosition(RobotBase robot)
        {
            ChangeWeather();
            return robotManager.GetRobotPosition(robot);
        }

        public virtual void LoadEnergy(RobotBase robot, int seconds)
        {
            robotManager.LoadEnergy(robot, seconds);
        }

        public virtual void RepairRobotPart(RobotBase robot, RobotPart robotPart)
        {
            robotManager.RepairRobotPart(robot, robotPart);
        }

        public virtual void ChangeWeather()
        {
            Random random = new Random();
            int weatherChange = random.Next(1, 101);

            if (weatherChange <= 40)
            {
                Weather = Weather.SUNNY;
            }
            else if (weatherChange <= 70)
            {
                Weather = Weather.CLOUDY;
            }
            else if (weatherChange <= 90)
            {
                Weather = Weather.RAINY;
            }
            else if (weatherChange <= 95)
            {
                Weather = Weather.WINDY;
            }
            else
            {
                Weather = Weather.FOGGY;
            }
        }
    }
}