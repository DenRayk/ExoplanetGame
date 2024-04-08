using ExoplanetGame.Robot;

namespace ExoplanetGame.Exoplanet
{
    public class RobotManager
    {
        private Dictionary<RobotBase, Position> robots = new();

        public int GetRobotCount()
        {
            return robots.Count;
        }

        public void RemoveRobot(RobotBase Robot)
        {
            robots.Remove(Robot);
        }

        public bool LandRobot(RobotBase Robot, Position landPosition, Topography topography)
        {
            if (!robots.ContainsKey(Robot) && CheckPosition(landPosition, topography))
            {
                robots.Add(Robot, landPosition);
                return true;
            }
            RemoveRobot(Robot);
            return false;
        }

        public Position MoveRobot(RobotBase Robot, Topography topography)
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

        public Direction RotateRobot(RobotBase robot, Rotation rotation)
        {
            Position robotPosition = robots[robot];
            return robotPosition.Rotate(rotation);
        }

        public Position GetRobotPosition(RobotBase robot)
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