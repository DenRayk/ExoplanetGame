using System;
using System.Collections.Generic;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Exoplanet
{
    public class RobotManager
    {
        private Dictionary<Robot.DefaultRobot, Position> robots = new();

        public int GetRobotCount()
        {
            return robots.Count;
        }

        public void RemoveRobot(Robot.DefaultRobot Robot)
        {
            robots.Remove(Robot);
        }

        public bool LandRobot(Robot.DefaultRobot Robot, Position landPosition, Topography topography)
        {
            if (!robots.ContainsKey(Robot) && CheckPosition(landPosition, topography))
            {
                robots.Add(Robot, landPosition);
                return true;
            }
            RemoveRobot(Robot);
            return false;
        }

        public Position MoveRobot(Robot.DefaultRobot Robot, Topography topography)
        {
            Position robotPosition = robots[Robot];
            Position newPosition = robotPosition.GetAdjacentPosition();

            if (CheckPosition(newPosition, topography))
            {
                robots[Robot] = newPosition;
                return newPosition;
            }
            RemoveRobot(Robot);
            return null;
        }

        public Direction RotateRobot(Robot.DefaultRobot robot, Rotation rotation)
        {
            Position robotPosition = robots[robot];
            return robotPosition.Rotate(rotation);
        }

        public Position GetRobotPosition(Robot.DefaultRobot robot)
        {
            return robots[robot];
        }

        private bool CheckPosition(Position position, Topography topography)
        {
            if (position == null) return false;

            if (position.X < 0 || position.X >= topography.PlanetSize.Width || position.Y < 0 || position.Y >= topography.PlanetSize.Height)
            {
                return false;
            }

            foreach (var robot in robots.Values)
            {
                if (robot.X == position.X && robot.Y == position.Y)
                {
                    return false;
                }
            }

            return true;
        }
    }
}