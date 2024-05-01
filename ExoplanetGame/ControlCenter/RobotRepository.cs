using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;

namespace ExoplanetGame.ControlCenter
{
    internal class RobotRepository : IRobotRepository
    {
        private static RobotRepository instance;
        private Dictionary<RobotBase, Position> robots;

        public virtual void OnRobotPositionUpdated(RobotBase robot, Position position)
        {
            RobotPositionUpdated?.Invoke(this, new RobotPositionEventArgs(robot, position));
        }

        public event EventHandler<RobotPositionEventArgs> RobotPositionUpdated;

        private RobotRepository()
        {
            robots = new Dictionary<RobotBase, Position>();
        }

        public static RobotRepository GetInstance()
        {
            if (instance == null)
            {
                instance = new RobotRepository();
            }
            return instance;
        }

        public void AddRobot(RobotBase robot, Position position)
        {
            robots.Add(robot, position);
        }

        public void RemoveRobot(RobotBase robot)
        {
            robots.Remove(robot);
        }

        public void MoveRobot(RobotBase robot, Position position)
        {
            robots[robot] = position;
        }

        public Position GetRobotPosition(RobotBase robot)
        {
            return robots[robot];
        }

        public Dictionary<RobotBase, Position> GetRobots()
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