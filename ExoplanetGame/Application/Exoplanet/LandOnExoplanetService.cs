using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Domain.Exoplanet;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class LandOnExoplanetService : LandOnExoplanetUseCase
    {
        private readonly ExoplanetService exoplanetService;

        public LandOnExoplanetService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public PositionResult LandExoplanet(IRobot robot, Position landPosition)
        {
            ExoPlanetBase exoPlanet = exoplanetService.ExoPlanet;
            PositionResult landResult = new();

            Position waterDriftPosition = exoplanetService.RobotPostionsService.WaterDrift(robot, landPosition, exoPlanet.Topography);

            if (!Equals(waterDriftPosition, landPosition))
            {
                landResult.AddMessage("Robot landed in water and drifted to a safe position.");
                landPosition = waterDriftPosition;
            }

            if (!exoPlanet.RobotPositionManager.Robots.ContainsKey(robot) && exoplanetService.RobotPostionsService.IsPositionSafeForRobot(robot, landPosition, exoPlanet.Topography, ref landResult))
            {
                exoPlanet.RobotPositionManager.Robots.Add(robot, landPosition);
                exoplanetService.HeatTracking.PerformAction(robot, RobotAction.LAND, exoPlanet.Topography, landPosition);
                exoplanetService.EnergyTracking.ConsumeEnergy(robot, RobotAction.LAND);

                landResult.IsSuccess = true;
                landResult.HasRobotSurvived = true;
                landResult.Position = landPosition;

                return landResult;
            }

            landResult.AddMessage("Robot cannot land.");

            return landResult;
        }
    }
}