using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Presentation.Commands.ControlCenter;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class MoveCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;
        private ControlCenterCommand controlCenterCommand;

        public MoveCommand(RobotBase robotBase, UCCollection ucCollection, BaseCommand previousCommand, ControlCenterCommand controlCenterCommand) : base(previousCommand, controlCenterCommand)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
            this.controlCenterCommand = controlCenterCommand;
        }

        public override void Execute()
        {
            PositionResult positionResult = ucCollection.UcCollectionRobot.MoveRobotService.Move(robotBase);

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

                if (!positionResult.HasRobotSurvived)
                {
                    controlCenterCommand.Execute();
                }
            }

            previousCommand.Execute();
        }
    }
}