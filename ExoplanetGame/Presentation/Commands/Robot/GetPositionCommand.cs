using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class GetPositionCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private IRobot robot;

        public GetPositionCommand(IRobot robot, UCCollection ucCollection)
        {
            this.robot = robot;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            PositionResult positionResult = ucCollection.UcCollectionRobot.GetPositionService.GetPosition(robot);
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