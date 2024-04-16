using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet
{
    public class RobotManager
    {
        internal readonly Dictionary<RobotBase, Position> robots;

        private readonly RobotStatusManager robotStatusManager;

        private readonly Topography topography;

        public RobotManager(Topography topography)
        {
            this.topography = topography;
            robots = new Dictionary<RobotBase, Position>();

            robotStatusManager = new RobotStatusManager();
            MoveController = new MoveController(this, robotStatusManager);
            LandController = new LandController(this, robotStatusManager);
            ScanController = new ScanController(this, robotStatusManager);
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
            robotStatusManager.RobotHeatTracker.PerformAction(robot, RobotAction.GETPOSITION, topography);
            return robots[robot];
        }

        public void LoadEnergy(RobotBase robot)
        {
            robotStatusManager.RobotEnergyTracker.LoadEnergy(robot);
            Console.WriteLine($"Energy loaded to {robotStatusManager.RobotEnergyTracker.GetEnergy(robot)}");
        }

        public void RemoveRobot(RobotBase robot)
        {
            robots.Remove(robot);
            Console.WriteLine("Robot removed from exoplanet.");
        }

        public Direction RotateRobot(RobotBase robot, Rotation rotation)
        {
            Position robotPosition = robots[robot];

            if (robotStatusManager.RobotPartsTracker.isRobotPartDamaged(robot, RobotParts.RIGHTMOTOR) && rotation == Rotation.RIGHT)
            {
                Console.WriteLine("The robot's right motor is damaged and can't rotate right.");
                return robotPosition.Direction;
            }

            if (robotStatusManager.RobotPartsTracker.isRobotPartDamaged(robot, RobotParts.LEFTMOTOR) && rotation == Rotation.LEFT)
            {
                Console.WriteLine("The robot's left motor is damaged and can't rotate left.");
                return robotPosition.Direction;
            }

            robotStatusManager.RobotHeatTracker.PerformAction(robot, RobotAction.ROTATE, topography);

            if (robotStatusManager.RobotStuckTracker.IsRobotStuck(robot))
            {
                robotStatusManager.RobotStuckTracker.UnstuckRobot(robot);
            }

            if (rotation == Rotation.RIGHT)
            {
                robotStatusManager.RobotPartsTracker.RobotPartDamage(robot, RobotParts.RIGHTMOTOR);
            }
            else
            {
                robotStatusManager.RobotPartsTracker.RobotPartDamage(robot, RobotParts.LEFTMOTOR);
            }

            return robotPosition.Rotate(rotation);
        }

        internal bool CanRobotMove(RobotBase robot)
        {
            bool isMovementSensorDamaged = robotStatusManager.RobotPartsTracker.isRobotPartDamaged(robot, RobotParts.MOVEMENTSENSOR);
            bool areWheelsDamaged = robotStatusManager.RobotPartsTracker.isRobotPartDamaged(robot, RobotParts.WHEELS);
            bool isRobotStuck = robotStatusManager.RobotStuckTracker.IsRobotStuck(robot);

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

            if (newGround == Ground.MUD || newGround == Ground.PLANT)
            {
                robotStatusManager.RobotStuckTracker.RobotGetStuckRandomly(robot);
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
            robotStatusManager.RobotHeatTracker.WaterCoolDown(robot);

            if (robot.RobotVariant == RobotVariant.AQUA)
            {
                return position;
            }

            while (position.Y < topography.PlanetSize.Height - 1 && topography.GetMeasureAtPosition(position).Ground == Ground.WATER)
            {
                position = new Position(position.X, position.Y + 1);
            }

            while (position.Y == topography.PlanetSize.Height - 1 && topography.GetMeasureAtPosition(position).Ground == Ground.WATER)
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