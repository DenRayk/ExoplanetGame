using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot
{
    internal class RobotScanService : RobotScanUseCase
    {
        private readonly ExoplanetService exoplanetService;
        private readonly IRobotRepository robotRepository;
        private readonly AddMeasureUseCase addMeasureService;

        public RobotScanService(ExoplanetService exoplanetService, AddMeasureUseCase addMeasureService)
        {
            this.exoplanetService = exoplanetService;
            this.addMeasureService = addMeasureService;
            this.robotRepository = RobotRepository.GetInstance();
        }

        public ScanResult Scan(IRobot robot)
        {
            ScanResult scanResult = exoplanetService.ScanOnExoplanetService.Scan(robot);

            if (scanResult.IsSuccess)
            {
                addMeasureService.AddMeasures(scanResult.Measures);
                return scanResult;
            }

            if (!scanResult.HasRobotSurvived)
            {
                robotRepository.RemoveRobot(robot);
                exoplanetService.RobotPostionsService.RemoveRobot(robot);
            }

            return scanResult;
        }
    }
}