using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class RotateCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;
        private Rotation rotation;

        public RotateCommand(RobotBase robotBase, UCCollection ucCollection, Rotation rotation)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
            this.rotation = rotation;
        }

        public override void Execute()
        {
            RotationResult rotationResult = ucCollection.UcCollectionRobot.RotateRobotService.Rotate(robotBase, rotation);
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