using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot
{
    internal class GetPositionService : GetPositionUseCase
    {
        private ExoplanetService exoplanetService;
        private RobotRepository robotRepository;

        public GetPositionService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
            robotRepository = RobotRepository.GetInstance();
        }

        public PositionResult GetPosition(IRobot robot)
        {
            PositionResult positionResult = exoplanetService.RobotPostionsService.GetRobotPosition(robot);

            if (!positionResult.HasRobotSurvived)
            {
                exoplanetService.RobotPostionsService.RemoveRobot(robot);
                robotRepository.RemoveRobot(robot);
            }
            return exoplanetService.RobotPostionsService.GetRobotPosition(robot);
        }
    }
}