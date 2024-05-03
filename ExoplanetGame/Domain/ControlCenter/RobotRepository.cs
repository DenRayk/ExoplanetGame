﻿using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;

namespace ExoplanetGame.Domain.ControlCenter
{
    internal class RobotRepository : IRobotRepository
    {
        private static RobotRepository instance;
        private Dictionary<IRobot, Position> robots;

        public virtual void OnRobotPositionUpdated(IRobot robot, Position position)
        {
            RobotPositionUpdated?.Invoke(this, new RobotPositionEventArgs(robot, position));
        }

        public event EventHandler<RobotPositionEventArgs> RobotPositionUpdated;

        private RobotRepository()
        {
            robots = new Dictionary<IRobot, Position>();
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

        public void RemoveRobot(IRobot robot)
        {
            robots.Remove(robot);
        }

        public void MoveRobot(IRobot robot, Position position)
        {
            robots[robot] = position;
            OnRobotPositionUpdated(robot, position);
        }

        public Position GetRobotPosition(RobotBase robot)
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