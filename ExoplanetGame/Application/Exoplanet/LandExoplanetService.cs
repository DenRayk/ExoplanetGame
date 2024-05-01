using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class LandExoplanetService : LandExoplanetUseCase
    {
        private readonly ExoplanetService exoplanetService;

        public LandExoplanetService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public PositionResult LandExoplanet(RobotBase robot, Position landPosition)
        {
            ExoPlanetBase exoPlanet = exoplanetService.ExoPlanet;
            PositionResult landResult = new();
            landPosition = exoplanetService.RobotPostionUseCase.WaterDrift(robot, landPosition, exoPlanet.Topography);

            if (!exoPlanet.RobotPositionManager.Robots.ContainsKey(robot) && exoplanetService.RobotPostionUseCase.IsPositionSafeForRobot(robot, landPosition, exoPlanet.Topography, ref landResult))
            {
                exoPlanet.RobotPositionManager.Robots.Add(robot, landPosition);
                exoplanetService.HeatTrackingUseCase.PerformAction(robot, RobotAction.LAND, exoPlanet.Topography, landPosition);
                exoplanetService.EnergyTrackingUseCase.ConsumeEnergy(robot, RobotAction.LAND);

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