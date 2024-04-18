using ExoplanetGame.Robot;

namespace ExoplanetGame.Exoplanet
{
    public class RobotFreezeTracker
    {
        private readonly Dictionary<RobotBase, bool> freezeTracker = new();
        private readonly Dictionary<RobotBase, DateTime> lastRobotMove = new();

        public void FreezeRobot(RobotBase robot)
        {
            freezeTracker[robot] = true;
        }

        public bool IsFrozen(RobotBase robot)
        {
            return freezeTracker.ContainsKey(robot);
        }

        public void UpdateLastMove(RobotBase robot)
        {
            lastRobotMove[robot] = DateTime.Now;
        }

        public DateTime GetLastMove(RobotBase robot)
        {
            if (lastRobotMove.ContainsKey(robot))
            {
                return lastRobotMove[robot];
            }
            else
            {
                return DateTime.Now;
            }
        }
    }
}