using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet
{
    public class RobotManager
    {
        internal readonly Dictionary<RobotBase, Position> robots;

        private readonly RobotHeatTracker robotHeatTracker;
        private readonly RobotPartsTracker robotPartsTracker;
        private readonly RobotStuckTracker robotStuckTracker;

        public RobotManager()
        {
            MoveController = new MoveController(this);
            robotHeatTracker = new RobotHeatTracker();
            robotPartsTracker = new RobotPartsTracker();
            robotStuckTracker = new RobotStuckTracker();
            robots = new Dictionary<RobotBase, Position>();
            LandController = new LandController(this);
            ScanController = new ScanController(this, robotPartsTracker, robotHeatTracker);
        }

        public LandController LandController { get; }
        public MoveController MoveController { get; }
        public ScanController ScanController { get; }

        public static bool IsPositionInBounds(Position position, Topography topography)
        {
            bool isXCoordinateInBounds = position.X >= 0;

            bool isXCoordinateLessThanWidth = position.X < topography.PlanetSize.Width;

            bool isYCoordinateInBounds = position.Y >= 0;

            bool isYCoordinateLessThanHeight = position.Y < topography.PlanetSize.Height;

            bool isPositionInBounds = isXCoordinateInBounds && isXCoordinateLessThanWidth && isYCoordinateInBounds && isYCoordinateLessThanHeight;

            return isPositionInBounds;
        }

        public int GetRobotCount()
        {
            return robots.Count;
        }

        public Position GetRobotPosition(RobotBase robot)
        {
            robotHeatTracker.PerformAction(robot);
            return robots[robot];
        }

        public bool IsPositionSafeForLavaBot(LavaBot lavaBot, Position newPosition, Topography topography)
        {
            if (newPosition == null) return false;

            if (!IsPositionInBounds(newPosition, topography))
            {
                Console.WriteLine("The position is out of bounds.");
                return false;
            }

            if (IsAnotherRobotAlreadyAtThisPosition(newPosition))
            {
                Console.WriteLine("Another robot is already at this position.");
                return false;
            }

            return true;
        }

        public void RemoveRobot(RobotBase robot)
        {
            robots.Remove(robot);
        }

        public Direction RotateRobot(RobotBase robot, Rotation rotation)
        {
            Position robotPosition = robots[robot];

            if (robotPartsTracker.isRobotPartDamaged(robot, RobotParts.RIGHTMOTOR) && rotation == Rotation.RIGHT)
            {
                Console.WriteLine("The robot's right motor is damaged and can't rotate right.");
                return robotPosition.Direction;
            }

            if (robotPartsTracker.isRobotPartDamaged(robot, RobotParts.LEFTMOTOR) && rotation == Rotation.LEFT)
            {
                Console.WriteLine("The robot's left motor is damaged and can't rotate left.");
                return robotPosition.Direction;
            }

            robotHeatTracker.PerformAction(robot);

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

        internal bool CanRobotMove(RobotBase robot)
        {
            return !robotPartsTracker.isRobotPartDamaged(robot, RobotParts.MOVEMENTSENSOR) &&
                   !robotPartsTracker.isRobotPartDamaged(robot, RobotParts.WHEELS);
        }

        internal void CheckIfRobotGetsStuck(RobotBase robot, Topography topography, Position newPosition)
        {
            Ground newGround = topography.GetMeasureAtPosition(newPosition).Ground;

            if (newGround == Ground.MORAST || newGround == Ground.PFLANZEN)
            {
                robotStuckTracker.RobotGetStuckRandomly(robot);
            }
        }

        internal Position GetNewRobotPosition(Position currentRobotPosition)
        {
            return currentRobotPosition.GetAdjacentPosition();
        }

        internal bool IsPositionSafeForRobot(RobotBase robot, Position newPosition, Topography topography)
        {
            if (newPosition == null) return false;

            if (!IsPositionInBounds(newPosition, topography))
            {
                Console.WriteLine("The position is out of bounds.");
                return false;
            }

            if (IsPositionLava(newPosition, topography))
            {
                Console.WriteLine("The floor is lava.");
                return false;
            }

            if (IsAnotherRobotAlreadyAtThisPosition(newPosition))
            {
                Console.WriteLine("Another robot is already at this position.");
                return false;
            }

            return true;
        }

        internal void UpdateRobotPosition(RobotBase robot, Position newPosition)
        {
            robots[robot] = newPosition;
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