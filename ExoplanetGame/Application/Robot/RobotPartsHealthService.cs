using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Robot
{
    internal class RobotPartsHealthService : RobotPartsHealthUseCase
    {
        private readonly ExoplanetService exoplanetService;

        public RobotPartsHealthService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public Dictionary<RobotPart, int> GetRobotPartsByRobot(IRobot robot)
        {
            return exoplanetService.RobotPartsTracking.GetRobotPartsByRobot(robot);
        }

        public void RepairRobotPart(IRobot robot, RobotPart robotPart)
        {
            exoplanetService.RobotPartsTracking.RepairRobotPart(robot, robotPart);
        }
    }
}