using ExoplanetGame.Robot;

namespace ExoplanetGame.Exoplanet
{
    public class RobotPartsTracker
    {
        private Dictionary<RobotBase, Dictionary<RobotPart, int>> robotParts = new();
        private Random random = new();

        public void RobotPartDamage(RobotBase robot, RobotPart part)
        {
            CreateRobotIfNotExists(robot);

            CreatePartIfNotExists(robot, part);

            robotParts[robot][part] -= random.Next(1, 10);
        }

        public bool isRobotPartDamaged(RobotBase robot, RobotPart part)
        {
            if (!DoesRobotExist(robot))
            {
                return false;
            }

            if (!DoesPartExist(robot, part))
            {
                return false;
            }

            return robotParts[robot][part] <= 0;
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

            return robotParts[robot][part];
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

            robotParts[robot][part] = 100;
        }

        private bool DoesRobotExist(RobotBase robot) => robotParts.ContainsKey(robot);

        private bool DoesPartExist(RobotBase robot, RobotPart robotPart)
        {
            if (!robotParts.ContainsKey(robot))
            {
                return false;
            }

            return robotParts[robot].ContainsKey(robotPart);
        }

        private void CreatePartIfNotExists(RobotBase robot, RobotPart robotPart)
        {
            if (!robotParts[robot].ContainsKey(robotPart))
            {
                robotParts[robot][robotPart] = robot.RobotInformation.RobotParts[robotPart];
            }
        }

        private void CreateRobotIfNotExists(RobotBase robot)
        {
            if (!robotParts.ContainsKey(robot))
            {
                robotParts[robot] = new();
            }
        }
    }
}