using ExoplanetGame.Application;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class ScanCommand : RobotCommand
    {
        private readonly UCCollection ucCollection;
        private readonly IRobot robot;

        public ScanCommand(IRobot robot, UCCollection ucCollection)
        {
            this.robot = robot;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            try
            {
                ScanResult scanResult = PerformRobotScan();

                if (scanResult.IsSuccess)
                {
                    foreach (KeyValuePair<Measure, Position> measure in scanResult.Measures)
                    {
                        Console.WriteLine($"Scanned {measure.Key} at {measure.Value}");
                    }
                }
                else
                {
                    Console.WriteLine(scanResult.Message);
                }
            }
            catch (RobotOverheatException exception)
            {
                HandleRobotOverheatException(exception);
            }
        }

        private ScanResult PerformRobotScan()
        {
            ScanResult scanResult = ucCollection.UcCollectionRobot.RobotScanService.Scan(robot);
            RobotResult = scanResult;
            return scanResult;
        }

        private void HandleRobotOverheatException(RobotOverheatException exception)
        {
            Console.WriteLine(exception.Message);

            RobotResult = new ScanResult
            {
                IsSuccess = false,
                HasRobotSurvived = true,
                Message = exception.Message
            };

            Console.WriteLine("Cooling down the robot...");

            ucCollection.UcCollectionRobot.RobotCoolDownService.CoolDownRobot(robot, robot.RobotInformation.MaxHeat / 10);

            Console.Clear();
        }
    }
}