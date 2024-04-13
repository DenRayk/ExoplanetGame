using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Exoplanet
{
    public class RobotManager
    {
        private Dictionary<RobotBase, Position> robots = new();
        private RobotHeatTracker robotHeatTracker = new();
        private RobotStuckTracker robotStuckTracker = new();
        private RobotPartsTracker robotPartsTracker = new();

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

        public Measure Scan(RobotBase robot, Topography topography)
        {
            robotHeatTracker.PerformAction(robot);
            robotPartsTracker.RobotPartDamage(robot, RobotParts.SCANSENSOR);
            return topography.GetMeasureAtPosition(robots[robot]);
        }

        public Position MoveRobot(RobotBase robot, Topography topography)
        {
            Position robotPosition = robots[robot];
            Position newPosition = robotPosition.GetAdjacentPosition();

            if (CheckPosition(robot, newPosition, topography))
            {
                if (robotStuckTracker.IsRobotStuck(robot))
                {
                    Console.WriteLine("The robot is stuck and can't move. Try to rotate to get unstuck.");
                    return robotPosition;
                }

                robotPartsTracker.RobotPartDamage(robot, RobotParts.MOVEMENTSENSOR);
                robotHeatTracker.PerformAction(robot);
                robots[robot] = newPosition;

                CheckIfRobotGetsStuck(robot, topography, newPosition);

                return newPosition;
            }

            RemoveRobot(robot);

            return null;
        }

        private void CheckIfRobotGetsStuck(RobotBase robot, Topography topography, Position newPosition)
        {
            Ground newGround = topography.GetMeasureAtPosition(newPosition).Ground;

            if (newGround == Ground.MORAST || newGround == Ground.PFLANZEN)
            {
                robotStuckTracker.RobotGetStuckRandomly(robot);
            }
        }

        public Direction RotateRobot(RobotBase robot, Rotation rotation)
        {
            robotHeatTracker.PerformAction(robot);
            Position robotPosition = robots[robot];

            if (robotStuckTracker.IsRobotStuck(robot))
            {
                robotStuckTracker.UnstuckRobot(robot);
            }

            if (rotation == Rotation.RIGHT)
            {
                robotPartsTracker.RobotPartDamage(robot, RobotParts.RIGHTMOTOR);
            }
            else
            {
                robotPartsTracker.RobotPartDamage(robot, RobotParts.LEFTMOTOR);
            }

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