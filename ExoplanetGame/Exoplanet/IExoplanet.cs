using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Exoplanet
{
    namespace ExoplanetGame.Exoplanet
    {
        public interface IExoPlanet
        {
            Topography Topography { get; set; }
            PlanetVariant PlanetVariant { get; }

            int GetRobotCount();

            void RemoveRobot(RobotBase Robot);

            PositionResult Land(RobotBase robot, Position landPosition);

            PositionResult Move(RobotBase robot);

            RotationResult Rotate(RobotBase robot, Rotation rotation);

            ScanResult Scan(RobotBase robot);

            ScoutScanResult ScoutScan(RobotBase robot);

            PositionResult GetRobotPosition(RobotBase robot);

            LoadResult LoadEnergy(RobotBase robot, int seconds);

            void RepairRobotPart(RobotBase robot, RobotPart robotPart);

            Dictionary<RobotPart, int> GetRobotPartsByRobot(RobotBase robotBase);

            void ChangeWeather();
        }
    }
}