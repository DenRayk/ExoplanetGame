using System;
using System.Collections.Generic;
using ExoplanetGame.RemoteRobot;

namespace ExoplanetGame.Exoplanet
{
    public class RobotManager
    {
        private Dictionary<RemoteRobot.RemoteRobot, Position> robots = new();

        public int GetRobotCount()
        {
            return robots.Count;
        }

        public void RemoveRobot(RemoteRobot.RemoteRobot remoteRobot)
        {
            robots.Remove(remoteRobot);
        }

        public bool LandRobot(RemoteRobot.RemoteRobot remoteRobot, Position landPosition, Topography topography)
        {
            if (!robots.ContainsKey(remoteRobot) && CheckPosition(landPosition, topography))
            {
                robots.Add(remoteRobot, landPosition);
                return true;
            }
            RemoveRobot(remoteRobot);
            return false;
        }

        public Position MoveRobot(RemoteRobot.RemoteRobot remoteRobot, Topography topography)
        {
            Position robotPosition = robots[remoteRobot];
            Position newPosition = robotPosition.GetAdjacentPosition();

            if (CheckPosition(newPosition, topography))
            {
                robots[remoteRobot] = newPosition;
                return newPosition;
            }
            RemoveRobot(remoteRobot);
            return null;
        }

        public Direction RotateRobot(RemoteRobot.RemoteRobot robot, Rotation rotation)
        {
            Position robotPosition = robots[robot];
            return robotPosition.Rotate(rotation);
        }

        public Position GetRobotPosition(RemoteRobot.RemoteRobot robot)
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