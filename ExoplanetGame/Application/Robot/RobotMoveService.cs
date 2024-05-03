using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot
{
    internal class RobotMoveService : RobotMoveUseCase
    {
        private ExoplanetService exoplanetService;
        private IRobotRepository robotRepository;

        public RobotMoveService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
            robotRepository = RobotRepository.GetInstance();
        }

        public PositionResult Move(IRobot robot)
        {
            PositionResult positionResult = exoplanetService.MoveOnExoplanetService.MoveRobot(robot);

            if (positionResult.IsSuccess)
            {
                robot.RobotInformation.Position = positionResult.Position;
                robotRepository.MoveRobot(robot, positionResult.Position);
                return positionResult;
            }

            if (!positionResult.HasRobotSurvived)
            {
                exoplanetService.RobotPostionsService.RemoveRobot(robot);
                robotRepository.RemoveRobot(robot);
            }

            return positionResult;
        }
    }
}