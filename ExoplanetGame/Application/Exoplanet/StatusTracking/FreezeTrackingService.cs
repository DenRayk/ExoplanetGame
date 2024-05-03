using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Exoplanet.StatusTracking
{
    internal class FreezeTrackingService : FreezeTrackingUseCase
    {
        private ExoplanetService exoplanetService;

        public FreezeTrackingService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public void FreezeRobot(IRobot robot)
        {
            exoplanetService.ExoPlanet.RobotStatusManager.RobotsFrozen[robot] = true;
        }

        public bool IsFrozen(IRobot robot)
        {
            return exoplanetService.ExoPlanet.RobotStatusManager.RobotsFrozen.ContainsKey(robot) && exoplanetService.ExoPlanet.RobotStatusManager.RobotsFrozen[robot];
        }

        public void UpdateLastMove(IRobot robot)
        {
            exoplanetService.ExoPlanet.RobotStatusManager.RobotsLastMove[robot] = DateTime.Now;
        }

        public DateTime GetLastMove(IRobot robot)
        {
            if (exoplanetService.ExoPlanet.RobotStatusManager.RobotsLastMove.ContainsKey(robot))
            {
                return exoplanetService.ExoPlanet.RobotStatusManager.RobotsLastMove[robot];
            }
            else
            {
                return DateTime.Now;
            }
        }
    }
}