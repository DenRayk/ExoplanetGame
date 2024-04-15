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

        private readonly Topography topography;

        public RobotManager(Topography topography)
        {
            robotHeatTracker = new RobotHeatTracker();
            robotPartsTracker = new RobotPartsTracker();
            robotStuckTracker = new RobotStuckTracker();
            robots = new Dictionary<RobotBase, Position>();
            MoveController = new MoveController(this, robotHeatTracker, robotPartsTracker);
            LandController = new LandController(this, robotHeatTracker);
            ScanController = new ScanController(this, robotPartsTracker, robotHeatTracker);
            this.topography = topography;
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
            robotHeatTracker.PerformAction(robot, RobotAction.GETPOSITION, topography);
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

            if (IsAnotherRobotAlreadyAtThisPosition(lavaBot, newPosition))
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

            robotHeatTracker.PerformAction(robot, RobotAction.ROTATE, topography);

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
            bool isMovementSensorDamaged = robotPartsTracker.isRobotPartDamaged(robot, RobotParts.MOVEMENTSENSOR);
            bool areWheelsDamaged = robotPartsTracker.isRobotPartDamaged(robot, RobotParts.WHEELS);
            bool isRobotStuck = robotStuckTracker.IsRobotStuck(robot);

            if (isMovementSensorDamaged)
            {
                Console.WriteLine("Movement sensor is damaged.");
            }

            if (areWheelsDamaged)
            {
                Console.WriteLine("Wheels are damaged.");
            }

            if (isRobotStuck)
            {
                Console.WriteLine("Robot is stuck. Try to rotate.");
            }

            bool canMove = !isMovementSensorDamaged && !areWheelsDamaged && !isRobotStuck;

            if (!canMove)
            {
                Console.WriteLine("Robot cannot move due to the above problem(s).");
            }

            return canMove;
        }

        internal void CheckIfRobotGetsStuck(RobotBase robot, Topography topography, Position newPosition)
        {
            if (robot.RobotVariant == RobotVariant.MUD)
                return;

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

            if (IsPositionLava(newPosition, topography) && robot.RobotVariant != RobotVariant.LAVA)
            {
                Console.WriteLine("The floor is lava.");
                return false;
            }

            if (IsAnotherRobotAlreadyAtThisPosition(robot, newPosition))
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

        internal Position WaterDrift(RobotBase robot, Position position, Topography topography)
        {
            robotHeatTracker.WaterCoolDown(robot);

            if (robot.RobotVariant == RobotVariant.AQUA)
            {
                return position;
            }

            while (position.Y < topography.PlanetSize.Height - 1 && topography.GetMeasureAtPosition(position).Ground == Ground.WASSER)
            {
                position = new Position(position.X, position.Y + 1);
            }

            while (position.Y == topography.PlanetSize.Height - 1 && topography.GetMeasureAtPosition(position).Ground == Ground.WASSER)
            {
                position = new Position(position.X + 1, position.Y);
            }

            return position;
        }

        private static bool IsPositionLava(Position position, Topography topography)
        {
            return topography.GetMeasureAtPosition(position).Ground == Ground.LAVA;
        }

        private bool IsAnotherRobotAlreadyAtThisPosition(RobotBase robotBase, Position position)
        {
            foreach (var robot in robots.Keys)
            {
                if (robot.Equals(robotBase)) continue;

                if (robots[robot].X == position.X && robots[robot].Y == position.Y)
                {
                    return true;
                }
            }
            return false;
        }
    }
}