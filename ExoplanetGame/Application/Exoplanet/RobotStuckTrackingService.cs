using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class RobotStuckTrackingService : RobotStuckTrackingUseCase
    {
        private ExoplanetService exoplanetService;

        public RobotStuckTrackingService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public void RobotGetStuckRandomly(RobotBase robot)
        {
            throw new NotImplementedException();
        }

        public bool IsRobotStuck(RobotBase robot)
        {
            throw new NotImplementedException();
        }

        public void UnstuckRobot(RobotBase robot)
        {
            throw new NotImplementedException();
        }
    }
}