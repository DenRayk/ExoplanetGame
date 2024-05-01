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
        private Random random = new();

        public RobotStuckTrackingService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public void RobotGetStuckRandomly(RobotBase robot)
        {
            if (random.Next(0, 100) < 30)
            {
                exoplanetService.ExoPlanet.RobotStatusManager.RobotsStuck[robot] = true;
            }
        }

        public bool IsRobotStuck(RobotBase robot)
        {
            return exoplanetService.ExoPlanet.RobotStatusManager.RobotsStuck.ContainsKey(robot) && exoplanetService.ExoPlanet.RobotStatusManager.RobotsStuck[robot];
        }

        public void UnstuckRobot(RobotBase robot)
        {
            exoplanetService.ExoPlanet.RobotStatusManager.RobotsStuck[robot] = false;
        }
    }
}