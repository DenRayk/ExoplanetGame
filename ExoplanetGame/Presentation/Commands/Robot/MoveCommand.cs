using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class MoveCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;

        public MoveCommand(RobotBase robotBase, UCCollection ucCollection)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            PositionResult positionResult = ucCollection.UcCollectionRobot.MoveRobotService.Move(robotBase);
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