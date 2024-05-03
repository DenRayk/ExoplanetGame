using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class MoveCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private IRobot robot;

        public MoveCommand(IRobot robot, UCCollection ucCollection)
        {
            this.robot = robot;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            PositionResult positionResult = ucCollection.UcCollectionRobot.MoveRobotService.Move(robot);
            RobotResult = positionResult;

            if (positionResult.IsSuccess)
            {
                if (positionResult.Message != null)
                {
                    Console.WriteLine($"{positionResult.Message}");
                }
                Console.WriteLine($"Robot moved to {positionResult.Position}");
            }
            else
            {
                Console.WriteLine($"{positionResult.Message}");
            }
        }
    }
}