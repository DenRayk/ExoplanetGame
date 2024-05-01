using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application.Exoplanet;

namespace ExoplanetGame.Application.Robot
{
    public class UCCollectionRobot
    {
        public RobotLandUseCase RobotLandService;
        public GetPositionUseCase GetPositionService;
        public RobotScanUseCase ScanExoplanetService;

        public UCCollectionRobot(ExoplanetService exoplanetService)
        {
            RobotLandService = new RobotLandService(exoplanetService);
            GetPositionService = new GetPositionService(exoplanetService);
            ScanExoplanetService = new RobotScanService(exoplanetService);
        }
    }
}