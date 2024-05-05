using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Robot
{
    public class RobotCoolDownService : RobotCoolDownUseCase
    {
        private const int COOL_DOWN_RATE = 5;

        private readonly ExoplanetService exoplanetService;

        public RobotCoolDownService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public void CoolDownRobot(IRobot robot, int seconds)
        {
            for (int i = 0; i < seconds; i++)
            {
                if (exoplanetService.ExoPlanet.RobotStatusManager.RobotHeatLevels[robot] > 0)
                {
                    exoplanetService.ExoPlanet.RobotStatusManager.RobotHeatLevels[robot] -= COOL_DOWN_RATE;
                }
                Thread.Sleep(200);
            }
        }
    }
}