using ExoplanetGame.Application;
using ExoplanetGame.Presentation.Commands.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class MoveCommand : BaseCommand
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

            if (positionResult.IsSuccess)
            {
                Console.WriteLine($"RobotPositionManager moved to {positionResult.Position}");
            }
            else
            {
                Console.WriteLine($"{positionResult.Message}");

                if (!positionResult.HasRobotSurvived)
                {
                    ControlCenterCommand controlCenterCommand = new(ucCollection);
                    controlCenterCommand.Execute();
                }
            }

            SelectRobotActionCommand selectRobotActionCommand = new(ucCollection, robotBase);
            selectRobotActionCommand.Execute();
        }
    }
}