using ExoplanetGame.Application;
using ExoplanetGame.Presentation.Commands.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class ScanCommand : BaseCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;

        public ScanCommand(RobotBase robotBase, UCCollection ucCollection)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
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
                    ControlCenterCommand controlCenterCommand = new(ucCollection);
                    controlCenterCommand.Execute();
                }
            }

            SelectRobotActionCommand selectRobotActionCommand = new SelectRobotActionCommand(ucCollection, robotBase);
            selectRobotActionCommand.Execute();
        }
    }
}