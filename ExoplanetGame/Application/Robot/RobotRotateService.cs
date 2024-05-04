using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot
{
    internal class RobotRotateService : RobotRotateUseCase
    {
        private ExoplanetService exoplanetService;
        private IRobotRepository robotRepository;

        public RobotRotateService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
            robotRepository = RobotRepository.GetInstance();
        }

        public RotationResult Rotate(IRobot robot, Rotation rotation)
        {
            RotationResult rotationResult = exoplanetService.RotateOnExoplanetService.Rotate(robot, rotation);

            if (rotationResult.IsSuccess)
            {
                UpdatePosition(robot, rotationResult);
                return rotationResult;
            }

            if (!rotationResult.HasRobotSurvived)
            {
                exoplanetService.RobotPostionsService.RemoveRobot(robot);
                robotRepository.RemoveRobot(robot);
            }

            return rotationResult;
        }

        private void UpdatePosition(IRobot robot, RotationResult rotationResult)
        {
            Position newPosition = new(robot.RobotInformation.Position.X, robot.RobotInformation.Position.Y, rotationResult.Direction);
            robot.RobotInformation.Position = newPosition;
            robotRepository.MoveRobot(robot, newPosition);
        }
    }
}