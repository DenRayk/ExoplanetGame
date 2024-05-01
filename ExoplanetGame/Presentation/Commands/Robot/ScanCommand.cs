using ExoplanetGame.Application;
using ExoplanetGame.Presentation.Commands.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class ScanCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;
        private ControlCenterCommand controlCenterCommand;

        public ScanCommand(RobotBase robotBase, UCCollection ucCollection, BaseCommand previousCommand, ControlCenterCommand controlCenterCommand) : base(previousCommand, controlCenterCommand)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
            this.controlCenterCommand = controlCenterCommand;
        }

        public override void Execute()
        {
            ScanResult scanResult = ucCollection.UcCollectionRobot.ScanExoplanetService.Scan(robotBase);

            if (scanResult.IsSuccess)
            {
                foreach (var measure in scanResult.Measures)
                {
                    Console.WriteLine($"Scanned {measure.Key} at {measure.Value}");
                }
            }
            else
            {
                Console.WriteLine($"{scanResult.Message}");

                if (!scanResult.HasRobotSurvived)
                {
                    controlCenterCommand.Execute();
                }
            }

            previousCommand.Execute();
        }
    }
}