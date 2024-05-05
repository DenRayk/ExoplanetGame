using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class RotateCommand : RobotCommand
    {
        private readonly UCCollection ucCollection;
        private readonly IRobot robot;
        private readonly Rotation rotation;

        public RotateCommand(IRobot robot, UCCollection ucCollection, Rotation rotation)
        {
            this.robot = robot;
            this.ucCollection = ucCollection;
            this.rotation = rotation;
        }

        public override void Execute()
        {
            try
            {
                RotationResult rotationResult = PerformRobotRotation();

                if (rotationResult.IsSuccess)
                {
                    Console.WriteLine($"Robot rotated to {rotationResult.Direction}");
                }
                else
                {
                    Console.WriteLine(rotationResult.Message);
                }
            }
            catch (RobotOverheatException exception)
            {
                HandleRobotOverheatException(exception);
            }
        }

        private RotationResult PerformRobotRotation()
        {
            RotationResult rotationResult = ucCollection.UcCollectionRobot.RotateRobotService.Rotate(robot, rotation);
            RobotResult = rotationResult;
            return rotationResult;
        }

        private void HandleRobotOverheatException(RobotOverheatException exception)
        {
            Console.WriteLine(exception.Message);

            RobotResult = new RotationResult
            {
                IsSuccess = false,
                HasRobotSurvived = true,
                Message = exception.Message
            };

            Console.WriteLine("Cooling down the robot...");

            ucCollection.UcCollectionRobot.RobotCoolDownService.CoolDownRobot(robot, robot.RobotInformation.MaxHeat / 10);

            Console.Clear();
        }
    }
}