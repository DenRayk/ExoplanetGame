using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Exoplanet.Variants;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;

namespace ExoplanetGameTest
{
    internal class MockPlanet : ExoPlanetBase
    {
        public Topography Topography { get; set; }
        public PlanetVariant PlanetVariant { get; }

        public MockPlanet()
        {
            Topography = new Topography(new string[]
            {
                "FFFFFFFF",
                "FFFFFFFF",
            });
        }

        public int GetRobotCount()
        {
            throw new NotImplementedException();
        }

        public void RemoveRobot(RobotBase Robot)
        {
            throw new NotImplementedException();
        }

        public PositionResult Land(RobotBase robot, Position landPosition)
        {
            return new PositionResult()
            {
                HasRobotSurvived = true,
                IsSuccess = true,
                Position = landPosition
            };
        }

        public PositionResult Move(RobotBase robot)
        {
            throw new NotImplementedException();
        }

        public RotationResult Rotate(RobotBase robot, Rotation rotation)
        {
            throw new NotImplementedException();
        }

        public ScanResult Scan(RobotBase robot)
        {
            throw new NotImplementedException();
        }

        public ScoutScanResult ScoutScan(RobotBase robot)
        {
            throw new NotImplementedException();
        }

        public PositionResult GetRobotPosition(RobotBase robot)
        {
            throw new NotImplementedException();
        }

        public LoadResult LoadEnergy(RobotBase robot, int seconds)
        {
            throw new NotImplementedException();
        }

        public void RepairRobotPart(RobotBase robot, RobotPart robotPart)
        {
            throw new NotImplementedException();
        }

        public Dictionary<RobotPart, int> GetRobotPartsByRobot(RobotBase robotBase)
        {
            throw new NotImplementedException();
        }

        public void ChangeWeather()
        {
            throw new NotImplementedException();
        }
    }
}