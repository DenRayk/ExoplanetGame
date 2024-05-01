using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot
{
    internal class RobotScanService : RobotScanUseCase
    {
        private ExoplanetService exoplanetService;
        private RobotRepository robotRepository;
        private Domain.ControlCenter.ControlCenter controlCenter;

        public RobotScanService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
            this.robotRepository = RobotRepository.GetInstance();
            this.controlCenter = Domain.ControlCenter.ControlCenter.GetInstance();
        }

        public ScanResult Scan(RobotBase robot)
        {
            ScanResult scanResult = exoplanetService.ScanOnExoplanetService.Scan(robot);

            if (scanResult.IsSuccess)
            {
                controlCenter.AddMeasures(scanResult.Measures);
                return scanResult;
            }
            else
            {
                if (!scanResult.HasRobotSurvived)
                {
                    robotRepository.RemoveRobot(robot);
                    exoplanetService.RobotPostionsService.RemoveRobot(robot);
                }
            }

            return scanResult;
        }
    }
}