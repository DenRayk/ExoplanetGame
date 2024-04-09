using ExoplanetGame.Robot;

namespace ExoplanetGame.Exoplanet
{
    public class RobotManager
    {
        private Dictionary<RobotBase, Position> robots = new();
        private RobotHeatTracker robotHeatTracker = new();

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

        public Position MoveRobot(RobotBase robot, Topography topography)
        {
            Position robotPosition = robots[robot];
            Position newPosition = robotPosition.GetAdjacentPosition();

            if (CheckPosition(newPosition, topography))
            {
                robotHeatTracker.PerformAction(robot);
                robots[robot] = newPosition;
                return newPosition;
            }
            RemoveRobot(robot);
            return null;
        }

        public Direction RotateRobot(RobotBase robot, Rotation rotation)
        {
            robotHeatTracker.PerformAction(robot);
            Position robotPosition = robots[robot];
            return robotPosition.Rotate(rotation);
        }

        public Position GetRobotPosition(RobotBase robot)
        {
            robotHeatTracker.PerformAction(robot);
            return robots[robot];
        }

        private bool CheckPosition(Position position, Topography topography)
        {
            if (position == null) return false;

            if (!IsPositionInBounds(position, topography))
                return false;

            if (IsPositionLava(position, topography))
                return false;

            foreach (var robot in robots.Values)
            {
                if (robot.X == position.X && robot.Y == position.Y)
                {
                    return false;
                }
            }

            return true;
        }

        private bool IsPositionInBounds(Position position, Topography topography)
        {
            return position.X >= 0 && position.X < topography.PlanetSize.Width && position.Y >= 0 && position.Y < topography.PlanetSize.Height;
        }

        private bool IsPositionLava(Position position, Topography topography)
        {
            return topography.GetMeasureAtPosition(position).Ground == Ground.LAVA;
        }
    }
}