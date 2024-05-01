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
        public LandRobotUseCase landRobotUseCase;
        public GetPositionUseCase GetPositionUseCase;

        public UCCollectionRobot(ExoplanetService exoplanetService)
        {
            landRobotUseCase = new LandRobotService(exoplanetService);
            GetPositionUseCase = new GetPositionService(exoplanetService);
        }
    }
}