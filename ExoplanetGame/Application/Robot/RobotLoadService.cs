using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot
{
    internal class RobotLoadService : RobotLoadUseCase
    {
        private ExoplanetService exoplanetService;
        private IRobotRepository robotRepository;

        public RobotLoadService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
            this.robotRepository = RobotRepository.GetInstance();
        }

        public LoadResult LoadEnergy(IRobot robot, int seconds)
        {
            LoadResult loadResult = exoplanetService.LoadOnExoplanetService.LoadEnergy(robot, seconds);

            if (!loadResult.HasRobotSurvived)
            {
                exoplanetService.RobotPostionsService.RemoveRobot(robot);
                robotRepository.RemoveRobot(robot);
            }

            return loadResult;
        }
    }
}