using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class LoadOnExoplanetService : LoadOnExoplanetUseCase
    {
        private ExoplanetService exoplanetService;

        public LoadOnExoplanetService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public LoadResult LoadEnergy(RobotBase robot, int seconds)
        {
            LoadResult loadResult = new LoadResult();

            if (!exoplanetService.RobotPartsTracking.IsRobotPartDamaged(robot, RobotPart.SOLARPANELS))
            {
                loadResult = exoplanetService.EnergyTracking.LoadEnergy(robot, seconds, exoplanetService.ExoPlanet.Weather);
                exoplanetService.RobotPartsTracking.RobotPartDamage(robot, RobotPart.SOLARPANELS);
            }
            else
            {
                loadResult.IsSuccess = false;
                loadResult.HasRobotSurvived = true;
                loadResult.Message = "The robot's solar panels are damaged and can't load energy.";
            }

            return loadResult;
        }
    }
}