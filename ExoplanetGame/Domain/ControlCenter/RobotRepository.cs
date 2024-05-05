using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Domain.ControlCenter
{
    public class RobotRepository : IRobotRepository
    {
        private static IRobotRepository instance;
        private readonly Dictionary<IRobot, Position> robots;

        private RobotRepository()
        {
            robots = new Dictionary<IRobot, Position>();
        }

        public static IRobotRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new RobotRepository();
            }
            return instance;
        }

        public void AddRobot(IRobot robot, Position position)
        {
            robots.Add(robot, position);
        }

        public void RemoveRobot(IRobot robot)
        {
            robots.Remove(robot);
        }

        public void MoveRobot(IRobot robot, Position position)
        {
            robots[robot] = position;
        }

        public Position GetRobotPosition(IRobot robot)
        {
            return robots[robot];
        }

        public Dictionary<IRobot, Position> GetRobots()
        {
            return robots;
        }

        public void Clear()
        {
            robots.Clear();
        }

        public int GetRobotCount()
        {
            return robots.Count;
        }
    }
}