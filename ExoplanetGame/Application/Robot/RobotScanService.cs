using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot
{
    internal class RobotScanService : RobotScanUseCase
    {
        private ExoplanetService exoplanetService;
        private RobotRepository robotRepository;
        private global::ExoplanetGame.ControlCenter.ControlCenter controlCenter;

        public RobotScanService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
            this.robotRepository = RobotRepository.GetInstance();
            this.controlCenter = global::ExoplanetGame.ControlCenter.ControlCenter.GetInstance();
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