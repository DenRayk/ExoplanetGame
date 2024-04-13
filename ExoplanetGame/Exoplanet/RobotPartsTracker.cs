using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Exoplanet
{
    internal class RobotPartsTracker
    {
        private Dictionary<RobotBase, Dictionary<RobotParts, int>> robotParts = new();
        private Random random = new();

        public void RobotPartDamage(RobotBase robot, RobotParts part)
        {
            if (!robotParts.ContainsKey(robot))
            {
                robotParts[robot] = new();
            }

            if (!robotParts[robot].ContainsKey(part))
            {
                robotParts[robot][part] = 100;
            }

            robotParts[robot][part] -= random.Next(1, 10);
        }

        public int GetRobotPartHealth(RobotBase robot, RobotParts part)
        {
            if (!robotParts.ContainsKey(robot))
            {
                return 100;
            }

            if (!robotParts[robot].ContainsKey(part))
            {
                return 100;
            }

            return robotParts[robot][part];
        }

        public void RepairRobotPart(RobotBase robot, RobotParts part)
        {
            if (!robotParts.ContainsKey(robot))
            {
                return;
            }

            if (!robotParts[robot].ContainsKey(part))
            {
                return;
            }

            robotParts[robot][part] = 100;
        }
    }
}