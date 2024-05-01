using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Robot
{
    internal class RobotPartsHealthService : RobotPartsHealthUseCase
    {
        private ExoplanetService exoplanetService;

        public RobotPartsHealthService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public Dictionary<RobotPart, int> GetRobotPartsByRobot(RobotBase robotBase)
        {
            return exoplanetService.RobotPartsTracking.GetRobotPartsByRobot(robotBase);
        }

        public void RepairRobotPart(RobotBase robotBase, RobotPart robotPart)
        {
            exoplanetService.RobotPartsTracking.RepairRobotPart(robotBase, robotPart);
        }
    }
}