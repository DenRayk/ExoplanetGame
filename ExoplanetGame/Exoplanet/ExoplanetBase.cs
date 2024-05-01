using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Exoplanet
{
    public class ExoPlanetBase : IExoPlanet
    {
        internal Weather Weather { get; set; }

        public RobotManager RobotManager { get; set; }

        public Topography Topography { get; set; }
        public PlanetVariant PlanetVariant { get; set; }

        public ExoPlanetBase()
        {
            PlanetVariant = PlanetVariant.GAIA;
            Weather = Weather.SUNNY;
            RobotManager = new RobotManager(this);
        }

        public ExoPlanetBase(PlanetVariant planetVariant)
        {
            PlanetVariant = planetVariant;
            Weather = Weather.SUNNY;
            RobotManager = new RobotManager(this);
        }

        public virtual int GetRobotCount()
        {
            return RobotManager.GetRobotCount();
        }

        public virtual void RemoveRobot(RobotBase Robot)
        {
            RobotManager.RemoveRobot(Robot);
        }

        public virtual PositionResult Land(RobotBase robot, Position landPosition)
        {
            return RobotManager.LandController.LandRobot(robot, landPosition, Topography);
        }

        public virtual PositionResult Move(RobotBase robot)
        {
            ChangeWeather();
            return RobotManager.MoveController.MoveRobot(robot, Topography);
        }

        public virtual RotationResult Rotate(RobotBase robot, Rotation rotation)
        {
            ChangeWeather();
            return RobotManager.RotateRobot(robot, rotation);
        }

        public virtual ScanResult Scan(RobotBase robot)
        {
            ChangeWeather();
            return RobotManager.ScanController.Scan(robot, Topography);
        }

        public virtual ScoutScanResult ScoutScan(RobotBase robot)
        {
            ChangeWeather();
            return RobotManager.ScanController.ScoutScan(robot, Topography);
        }

        public virtual PositionResult GetRobotPosition(RobotBase robot)
        {
            ChangeWeather();
            return RobotManager.GetRobotPosition(robot);
        }

        public virtual LoadResult LoadEnergy(RobotBase robot, int seconds)
        {
            return RobotManager.LoadEnergy(robot, seconds);
        }

        public virtual void RepairRobotPart(RobotBase robot, RobotPart robotPart)
        {
            RobotManager.RepairRobotPart(robot, robotPart);
        }

        public virtual Dictionary<RobotPart, int> GetRobotPartsByRobot(RobotBase robotBase)
        {
            return RobotManager.GetRobotPartsByRobot(robotBase);
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