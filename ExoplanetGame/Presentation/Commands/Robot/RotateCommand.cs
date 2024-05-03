using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class RotateCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private IRobot robot;
        private Rotation rotation;

        public RotateCommand(IRobot robot, UCCollection ucCollection, Rotation rotation)
        {
            this.robot = robot;
            this.ucCollection = ucCollection;
            this.rotation = rotation;
        }

        public override void Execute()
        {
            RotationResult rotationResult = ucCollection.UcCollectionRobot.RotateRobotService.Rotate(robot, rotation);
            RobotResult = rotationResult;

            if (rotationResult.IsSuccess)
            {
                Console.WriteLine($"Robot rotated to {rotationResult.Direction}");
            }
            else
            {
                Console.WriteLine($"{rotationResult.Message}");
            }
        }
    }
}