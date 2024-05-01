using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class LandExoplanetService : LandExoplanetUseCase
    {
        private ExoplanetService exoplanetService;

        public LandExoplanetService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public PositionResult LandExoplanet(RobotBase robot, Position landPosition)
        {
            IExoPlanet exoPlanet = exoplanetService.ExoPlanet;
            PositionResult landResult = new();
            landPosition = WaterDrift(robot, landPosition, exoPlanet);

            if (!exoPlanet.RobotManager.robots.ContainsKey(robot) && exoPlanet.RobotManager.IsPositionSafeForRobot(robot, landPosition, exoPlanet.Topography, ref landResult))
            {
                exoPlanet.RobotManager.robots.Add(robot, landPosition);
                exoPlanet.RobotManager.RobotStatusManager.RobotHeatTracker.PerformAction(robot, RobotAction.LAND, exoPlanet.Topography, landPosition);
                exoPlanet.RobotManager.RobotStatusManager.RobotEnergyTracker.ConsumeEnergy(robot, RobotAction.LAND);

                landResult.IsSuccess = true;
                landResult.HasRobotSurvived = true;
                landResult.Position = landPosition;
                landResult.AddMessage("Robot landed successful");

                return landResult;
            }

            landResult.AddMessage("Robot cannot land.");

            return landResult;
        }

        private Position WaterDrift(RobotBase robot, Position landPosition, IExoPlanet exoplanet)
        {
            WaterCoolDown(robot, exoplanet);

            if (robot.RobotVariant == RobotVariant.AQUA)
            {
                return landPosition;
            }

            while (landPosition.Y < exoplanet.Topography.PlanetSize.Height - 1 && exoplanet.Topography.GetMeasureAtPosition(landPosition).Ground == Ground.WATER)
            {
                landPosition = new Position(landPosition.X, landPosition.Y + 1);
            }

            while (landPosition.Y == exoplanet.Topography.PlanetSize.Height - 1 && exoplanet.Topography.GetMeasureAtPosition(landPosition).Ground == Ground.WATER)
            {
                landPosition = new Position(landPosition.X + 1, landPosition.Y);
            }

            return landPosition;
        }

        private void WaterCoolDown(RobotBase robot, IExoPlanet exoPlanet)
        {
            if (exoPlanet.RobotManager.RobotStatusManager.RobotHeatTracker.HeatLevels.ContainsKey(robot))
            {
                exoPlanet.RobotManager.RobotStatusManager.RobotHeatTracker.HeatLevels[robot] = 0;
            }
        }
    }
}