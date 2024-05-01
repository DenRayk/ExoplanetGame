using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class GetPositionCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;

        public GetPositionCommand(RobotBase robotBase, UCCollection ucCollection)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            PositionResult positionResult = ucCollection.UcCollectionRobot.GetPositionService.GetPosition(robotBase);
            RobotResult = positionResult;

            if (positionResult.IsSuccess)
            {
                Console.WriteLine($"Robot is at {positionResult.Position}");
            }
            else
            {
                Console.WriteLine($"{RobotResult.Message}");
            }
        }
    }
}