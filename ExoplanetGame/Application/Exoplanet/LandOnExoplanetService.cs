using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class LandOnExoplanetService : LandOnExoplanetUseCase
    {
        private readonly ExoplanetService exoplanetService;

        public LandOnExoplanetService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public PositionResult LandExoplanet(RobotBase robot, Position landPosition)
        {
            ExoPlanetBase exoPlanet = exoplanetService.ExoPlanet;
            PositionResult landResult = new();
            landPosition = exoplanetService.RobotPostionsService.WaterDrift(robot, landPosition, exoPlanet.Topography);

            if (!exoPlanet.RobotPositionManager.Robots.ContainsKey(robot) && exoplanetService.RobotPostionsService.IsPositionSafeForRobot(robot, landPosition, exoPlanet.Topography, ref landResult))
            {
                exoPlanet.RobotPositionManager.Robots.Add(robot, landPosition);
                exoplanetService.HeatTracking.PerformAction(robot, RobotAction.LAND, exoPlanet.Topography, landPosition);
                exoplanetService.EnergyTracking.ConsumeEnergy(robot, RobotAction.LAND);

                landResult.IsSuccess = true;
                landResult.HasRobotSurvived = true;
                landResult.Position = landPosition;
                landResult.AddMessage("RobotPositionManager landed successful");

                return landResult;
            }

            landResult.AddMessage("RobotPositionManager cannot land.");

            return landResult;
        }
    }
}