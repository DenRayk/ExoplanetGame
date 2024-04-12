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

        public void RemoveRobot(RobotBase robot)
        {
            robots.Remove(robot);
        }

        public bool LandRobot(RobotBase robot, Position landPosition, Topography topography)
        {
            if (!robots.ContainsKey(robot) && CheckPosition(robot, landPosition, topography))
            {
                robots.Add(robot, landPosition);
                return true;
            }

            RemoveRobot(robot);

            return false;
        }

        public Position MoveRobot(RobotBase robot, Topography topography)
        {
            Position robotPosition = robots[robot];
            Position newPosition = robotPosition.GetAdjacentPosition();

            if (CheckPosition(robot, newPosition, topography))
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

        private bool CheckPosition(RobotBase robot, Position position, Topography topography)
        {
            if (position == null) return false;

            if (!IsPositionInBounds(position, topography))
            {
                Console.WriteLine("The position is out of bounds.");
                return false;
            }

            if (IsPositionLava(position, topography) && robot.RobotVariant != RobotVariant.LAVA)
            {
                Console.WriteLine("The floor is lava.");
                return false;
            }

            if (IsAnotherRobotAlreadyAtThisPosition(position))
            {
                Console.WriteLine("Another robot is already at this position.");
                return false;
            }

            return true;
        }

        public bool IsPositionInBounds(Position position, Topography topography)
        {
            bool isXCoordinateInBounds = position.X >= 0;

            bool isXCoordinateLessThanWidth = position.X < topography.PlanetSize.Width;

            bool isYCoordinateInBounds = position.Y >= 0;

            bool isYCoordinateLessThanHeight = position.Y < topography.PlanetSize.Height;

            bool isPositionInBounds = isXCoordinateInBounds && isXCoordinateLessThanWidth && isYCoordinateInBounds && isYCoordinateLessThanHeight;

            return isPositionInBounds;
        }

        private static bool IsPositionLava(Position position, Topography topography)
        {
            return topography.GetMeasureAtPosition(position).Ground == Ground.LAVA;
        }

        private bool IsAnotherRobotAlreadyAtThisPosition(Position position)
        {
            foreach (var robot in robots.Values)
            {
                if (robot.X == position.X && robot.Y == position.Y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}