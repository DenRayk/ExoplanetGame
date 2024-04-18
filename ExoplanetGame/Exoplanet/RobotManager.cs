using ExoplanetGame.Exoplanet.Controller;
using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet
{
    public class RobotManager
    {
        internal readonly Dictionary<RobotBase, Position> robots;

        internal readonly RobotStatusManager robotStatusManager;

        private readonly ExoplanetBase exoplanet;

        public RobotManager(ExoplanetBase exoplanet)
        {
            this.exoplanet = exoplanet;
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

        public PositionResult GetRobotPosition(RobotBase robot)
        {
            PositionResult positionResult = new PositionResult();
            robotStatusManager.RobotHeatTracker.PerformAction(robot, RobotAction.GETPOSITION, exoplanet.Topography);

            positionResult.Position = robots[robot];
            positionResult.IsSuccess = true;
            return positionResult;
        }

        public LoadResult LoadEnergy(RobotBase robot, int seconds)
        {
            LoadResult loadResult = new LoadResult();

            if (!robotStatusManager.RobotPartsTracker.isRobotPartDamaged(robot, RobotPart.SOLARPANELS))
            {
                loadResult = robotStatusManager.RobotEnergyTracker.LoadEnergy(robot, seconds, exoplanet.Weather);
                robotStatusManager.RobotPartsTracker.RobotPartDamage(robot, RobotPart.SOLARPANELS);
            }
            else
            {
                loadResult.IsSuccess = false;
                loadResult.HasRobotSurvived = true;
                loadResult.Message = "The robot's solar panels are damaged and can't load energy.";
            }

            return loadResult;
        }

        public void RemoveRobot(RobotBase robot)
        {
            robots.Remove(robot);
        }

        public RotationResult RotateRobot(RobotBase robot, Rotation rotation)
        {
            RotationResult rotationResult = new RotationResult();
            Position robotPosition = robots[robot];

            if (robotStatusManager.RobotPartsTracker.isRobotPartDamaged(robot, RobotPart.RIGHTMOTOR) && rotation == Rotation.RIGHT)
            {
                rotationResult.Direction = robotPosition.Direction;
                rotationResult.IsSuccess = false;
                rotationResult.HasRobotSurvived = true;
                rotationResult.Message = "The robot's right motor is damaged and can't rotate right.";
                return rotationResult;
            }

            if (robotStatusManager.RobotPartsTracker.isRobotPartDamaged(robot, RobotPart.LEFTMOTOR) && rotation == Rotation.LEFT)
            {
                rotationResult.Direction = robotPosition.Direction;
                rotationResult.IsSuccess = false;
                rotationResult.HasRobotSurvived = true;
                rotationResult.Message = "The robot's left motor is damaged and can't rotate left.";
                return rotationResult;
            }

            robotStatusManager.RobotHeatTracker.PerformAction(robot, RobotAction.ROTATE, exoplanet.Topography);

            if (robotStatusManager.RobotStuckTracker.IsRobotStuck(robot))
            {
                robotStatusManager.RobotStuckTracker.UnstuckRobot(robot);
            }

            if (rotation == Rotation.RIGHT)
            {
                robotStatusManager.RobotPartsTracker.RobotPartDamage(robot, RobotPart.RIGHTMOTOR);
            }
            else
            {
                robotStatusManager.RobotPartsTracker.RobotPartDamage(robot, RobotPart.LEFTMOTOR);
            }

            rotationResult.IsSuccess = true;
            rotationResult.HasRobotSurvived = true;
            rotationResult.Direction = robotPosition.Rotate(rotation);

            return rotationResult;
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

        internal bool IsPositionSafeForRobot(RobotBase robot, Position newPosition, Topography topography, ref PositionResult positionResult)
        {
            if (newPosition == null) return false;

            if (!IsPositionInBounds(newPosition, topography))
            {
                positionResult.Message = "The position is out of bounds.\n";
                positionResult.IsSuccess = false;
                positionResult.HasRobotSurvived = false;
                return false;
            }

            if (IsPositionLava(newPosition, topography) && robot.RobotVariant != RobotVariant.LAVA)
            {
                positionResult.Message = "The position is lava.\n";
                positionResult.IsSuccess = false;
                positionResult.HasRobotSurvived = false;
                return false;
            }

            if (IsAnotherRobotAlreadyAtThisPosition(robot, newPosition))
            {
                positionResult.Message = "Another robot is already at this position.\n";
                positionResult.IsSuccess = false;
                positionResult.HasRobotSurvived = false;
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

        public void RepairRobotPart(RobotBase robot, RobotPart robotPart)
        {
            robotStatusManager.RobotPartsTracker.RepairRobotPart(robot, robotPart);
        }
    }
}