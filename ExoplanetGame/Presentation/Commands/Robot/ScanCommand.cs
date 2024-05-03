using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class ScanCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private IRobot robot;

        public ScanCommand(IRobot robot, UCCollection ucCollection)
        {
            this.robot = robot;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            ScanResult scanResult = ucCollection.UcCollectionRobot.RobotScanService.Scan(robot);
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