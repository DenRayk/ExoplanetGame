using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Domain.Robot;

namespace ExoplanetGame.Application.Exoplanet.StatusTracking
{
    internal class RobotPartsTrackingService : RobotPartsTrackingUseCase
    {
        private ExoplanetService exoplanetService;
        private Random random = new Random();

        public RobotPartsTrackingService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public void RobotPartDamage(RobotBase robot, RobotPart part)
        {
            CreateRobotIfNotExists(robot);

            CreatePartIfNotExists(robot, part);

            exoplanetService.ExoPlanet.RobotStatusManager.RobotPartsHealth[robot][part] -= random.Next(1, 10);
        }

        private void CreatePartIfNotExists(RobotBase robot, RobotPart robotPart)
        {
            if (!exoplanetService.ExoPlanet.RobotStatusManager.RobotPartsHealth[robot].ContainsKey(robotPart))
            {
                exoplanetService.ExoPlanet.RobotStatusManager.RobotPartsHealth[robot][robotPart] = robot.RobotInformation.RobotParts[robotPart];
            }
        }

        private void CreateRobotIfNotExists(RobotBase robot)
        {
            if (!exoplanetService.ExoPlanet.RobotStatusManager.RobotPartsHealth.ContainsKey(robot))
            {
                exoplanetService.ExoPlanet.RobotStatusManager.RobotPartsHealth[robot] = new();
            }
        }

        public bool IsRobotPartDamaged(RobotBase robot, RobotPart part)
        {
            if (!DoesRobotExist(robot))
            {
                return false;
            }

            if (!DoesPartExist(robot, part))
            {
                return false;
            }

            return exoplanetService.ExoPlanet.RobotStatusManager.RobotPartsHealth[robot][part] <= 0;
        }

        private bool DoesPartExist(RobotBase robot, RobotPart robotPart)
        {
            if (!exoplanetService.ExoPlanet.RobotStatusManager.RobotPartsHealth.ContainsKey(robot))
            {
                return false;
            }

            return exoplanetService.ExoPlanet.RobotStatusManager.RobotPartsHealth[robot].ContainsKey(robotPart);
        }

        private bool DoesRobotExist(RobotBase robot)
        {
            return exoplanetService.ExoPlanet.RobotStatusManager.RobotPartsHealth.ContainsKey(robot);
        }

        public int GetRobotPartHealth(RobotBase robot, RobotPart part)
        {
            if (!DoesRobotExist(robot))
            {
                return 0;
            }

            if (!DoesPartExist(robot, part))
            {
                return 0;
            }

            return exoplanetService.ExoPlanet.RobotStatusManager.RobotPartsHealth[robot][part];
        }

        public Dictionary<RobotPart, int> GetRobotPartsByRobot(RobotBase robot)
        {
            if (DoesRobotExist(robot))
                return exoplanetService.ExoPlanet.RobotStatusManager.RobotPartsHealth[robot];

            return null;
        }

        public void RepairRobotPart(RobotBase robot, RobotPart part)
        {
            if (!DoesRobotExist(robot))
            {
                return;
            }

            if (!DoesPartExist(robot, part))
            {
                return;
            }

            exoplanetService.ExoPlanet.RobotStatusManager.RobotPartsHealth[robot][part] = 100;
        }
    }
}