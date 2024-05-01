using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;

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

        public RotationResult Rotate(RobotBase robot, Rotation rotation)
        {
            RotationResult rotationResult = exoplanetService.RotateOnExoplanetService.Rotate(robot, rotation);

            if (rotationResult.IsSuccess)
            {
                updatePosition(robot, rotationResult);
                return rotationResult;
            }

            if (!rotationResult.HasRobotSurvived)
            {
                exoplanetService.RobotPostionsService.RemoveRobot(robot);
                robotRepository.RemoveRobot(robot);
            }

            return rotationResult;
        }

        private void updatePosition(RobotBase robot, RotationResult rotationResult)
        {
            Position newPosition = new(robot.RobotInformation.Position.X, robot.RobotInformation.Position.Y, rotationResult.Direction);
            robot.RobotInformation.Position = newPosition;
            robotRepository.MoveRobot(robot, newPosition);
        }
    }
}