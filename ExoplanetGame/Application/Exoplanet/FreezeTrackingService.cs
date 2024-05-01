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
            exoplanetService.ExoPlanet.RobotStatusManager.RobotsFrozen[robot] = true;
        }

        public bool IsFrozen(RobotBase robot)
        {
            return exoplanetService.ExoPlanet.RobotStatusManager.RobotsFrozen.ContainsKey(robot) && exoplanetService.ExoPlanet.RobotStatusManager.RobotsFrozen[robot];
        }

        public void UpdateLastMove(RobotBase robot)
        {
            exoplanetService.ExoPlanet.RobotStatusManager.RobotsLastMove[robot] = DateTime.Now;
        }

        public DateTime GetLastMove(RobotBase robot)
        {
            if (exoplanetService.ExoPlanet.RobotStatusManager.RobotsLastMove.ContainsKey(robot))
            {
                return exoplanetService.ExoPlanet.RobotStatusManager.RobotsLastMove[robot];
            }
            else
            {
                return DateTime.Now;
            }
        }
    }
}