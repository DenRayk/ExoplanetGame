using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Exoplanet
{
    internal class RobotStuckTracker
    {
        private Dictionary<RobotBase, bool> robotStuck = new();
        private Random random = new();

        public void RobotGetStuckRandomly(RobotBase robot)
        {
            if (random.Next(0, 100) < 30)
            {
                robotStuck[robot] = true;
            }
        }

        public bool IsRobotStuck(RobotBase robot)
        {
            return robotStuck.ContainsKey(robot) && robotStuck[robot];
        }

        public void UnstuckRobot(RobotBase robot)
        {
            Console.WriteLine("Robot is unstuck");
            robotStuck[robot] = false;
        }
    }
}