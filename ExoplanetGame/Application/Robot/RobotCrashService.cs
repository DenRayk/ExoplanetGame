using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot
{
    internal class RobotCrashService : RobotCrashUseCase
    {
        private readonly IRobotRepository robotRepository;
        private readonly ExoplanetService exoplanetService;

        public RobotCrashService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
            robotRepository = RobotRepository.GetInstance();
        }

        public RobotResultBase Crash(IRobot robot)
        {
            robotRepository.RemoveRobot(robot);
            exoplanetService.RobotPostionsService.RemoveRobot(robot);
            return new RobotResultBase { IsSuccess = true, HasRobotSurvived = false };
        }
    }
}