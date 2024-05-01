using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class RobotPartsTrackingService : RobotPartsTrackingUseCase
    {
        private ExoplanetService exoplanetService;

        public RobotPartsTrackingService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public void RobotPartDamage(RobotBase robot, RobotPart part)
        {
            throw new NotImplementedException();
        }

        public bool IsRobotPartDamaged(RobotBase robot, RobotPart part)
        {
            throw new NotImplementedException();
        }

        public int GetRobotPartHealth(RobotBase robot, RobotPart part)
        {
            throw new NotImplementedException();
        }

        public Dictionary<RobotPart, int> GetRobotPartsByRobot(RobotBase robot)
        {
            throw new NotImplementedException();
        }

        public void RepairRobotPart(RobotBase robot, RobotPart part)
        {
            throw new NotImplementedException();
        }
    }
}