using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class ScanCommand : RobotCommand
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
            RobotResult = scanResult;

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
            }
        }
    }
}