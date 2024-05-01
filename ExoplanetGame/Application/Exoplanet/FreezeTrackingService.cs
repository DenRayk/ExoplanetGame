using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class FreezeTrackingService : FreezeTrackingUseCase
    {
        private ExoplanetService exoplanetService;

        public FreezeTrackingService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public void FreezeRobot(RobotBase robot)
        {
            throw new NotImplementedException();
        }

        public bool IsFrozen(RobotBase robot)
        {
            throw new NotImplementedException();
        }

        public void UpdateLastMove(RobotBase robot)
        {
            throw new NotImplementedException();
        }

        public DateTime GetLastMove(RobotBase robot)
        {
            throw new NotImplementedException();
        }
    }
}