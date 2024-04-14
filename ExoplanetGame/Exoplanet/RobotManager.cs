using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet
{
    public class RobotManager
    {
        private readonly RobotHeatTracker robotHeatTracker = new();
        private readonly RobotPartsTracker robotPartsTracker = new();
        private readonly Dictionary<RobotBase, Position> robots = new();
        private readonly RobotStuckTracker robotStuckTracker = new();

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

        public bool LandLavaBot(LavaBot lavaBot, Position landPosition, Topography topography)
        {
            if (!robots.ContainsKey(lavaBot) && IsPositionSafeForLavaBot(lavaBot, landPosition, topography))
            {
                robots.Add(lavaBot, landPosition);
                return true;
            }

            RemoveRobot(lavaBot);

            return false;
        }

        public bool LandRobot(RobotBase robot, Position landPosition, Topography topography)
        {
            if (!robots.ContainsKey(robot) && IsPositionSafeForRobot(robot, landPosition, topography))
            {
                robots.Add(robot, landPosition);
                return true;
            }

            RemoveRobot(robot);

            return false;
        }

        public Position MoveLavaBot(LavaBot lavaBot, Topography topography)
        {
            if (CanRobotMove(lavaBot))
            {
                Position robotPosition = robots[lavaBot];
                Position newPosition = GetNewRobotPosition(robotPosition);

                if (IsPositionSafeForLavaBot(lavaBot, newPosition, topography))
                {
                    UpdateRobotPosition(lavaBot, newPosition);
                    CheckIfRobotGetsStuck(lavaBot, topography, newPosition);
                    return newPosition;
                }
                else
                {
                    RemoveRobot(lavaBot);
                    return null;
                }
            }
            else
            {
                Console.WriteLine("The robot's movement sensors or wheels are damaged and can't move.");
                return null;
            }
        }

        public Position MoveRobot(RobotBase robot, Topography topography)
        {
            if (CanRobotMove(robot))
            {
                Position robotPosition = robots[robot];
                Position newPosition = GetNewRobotPosition(robotPosition);

                if (IsPositionSafeForRobot(robot, newPosition, topography))
                {
                    UpdateRobotPosition(robot, newPosition);
                    CheckIfRobotGetsStuck(robot, topography, newPosition);
                    return newPosition;
                }
                else
                {
                    RemoveRobot(robot);
                    return null;
                }
            }
            else
            {
                Console.WriteLine("The robot's movement sensors or wheels are damaged and can't move.");
                return null;
            }
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

        public Measure Scan(RobotBase robot, Topography topography)
        {
            if (!robotPartsTracker.isRobotPartDamaged(robot, RobotParts.SCANSENSOR))
            {
                robotHeatTracker.PerformAction(robot);
                robotPartsTracker.RobotPartDamage(robot, RobotParts.SCANSENSOR);
                return topography.GetMeasureAtPosition(robots[robot]);
            }
            else
            {
                Console.WriteLine("The robot's scan sensor is damaged and can't scan.");
                return null;
            }
        }

        public Dictionary<Measure, Position> ScoutScan(RobotBase robot, Topography topography)
        {
            if (!robotPartsTracker.isRobotPartDamaged(robot, RobotParts.SCANSENSOR))
            {
                Dictionary<Measure, Position> scoutScanResults = new Dictionary<Measure, Position>();
                Position currentRobotPosition = GetRobotPosition(robot);

                Measure currentMeasure = topography.GetMeasureAtPosition(currentRobotPosition);
                scoutScanResults.Add(currentMeasure, currentRobotPosition);

                if (robot.RobotVariant == RobotVariant.SCOUT)
                {
                    Position firstForwardPosition = currentRobotPosition.GetAdjacentPosition();
                    Position secondForwardPosition = firstForwardPosition.GetAdjacentPosition();

                    bool isFirstPositionValid = IsPositionInBounds(firstForwardPosition, topography);
                    bool isSecondPositionValid = IsPositionInBounds(secondForwardPosition, topography);

                    if (isFirstPositionValid)
                    {
                        Measure firstPositionMeasure = topography.GetMeasureAtPosition(firstForwardPosition);
                        scoutScanResults.Add(firstPositionMeasure, firstForwardPosition);
                    }
                    if (isSecondPositionValid)
                    {
                        Measure secondPositionMeasure = topography.GetMeasureAtPosition(secondForwardPosition);
                        scoutScanResults.Add(secondPositionMeasure, secondForwardPosition);
                    }
                }

                return scoutScanResults;
            }
            else
            {
                Console.WriteLine("The robot's scan sensor is damaged and can't scan.");
                return null;
            }
        }

        private static bool IsPositionLava(Position position, Topography topography)
        {
            return topography.GetMeasureAtPosition(position).Ground == Ground.LAVA;
        }

        private bool CanRobotMove(RobotBase robot)
        {
            return !robotPartsTracker.isRobotPartDamaged(robot, RobotParts.MOVEMENTSENSOR) &&
                   !robotPartsTracker.isRobotPartDamaged(robot, RobotParts.WHEELS);
        }

        private void CheckIfRobotGetsStuck(RobotBase robot, Topography topography, Position newPosition)
        {
            Ground newGround = topography.GetMeasureAtPosition(newPosition).Ground;

            if (newGround == Ground.MORAST || newGround == Ground.PFLANZEN)
            {
                robotStuckTracker.RobotGetStuckRandomly(robot);
            }
        }

        private Position GetNewRobotPosition(Position currentRobotPosition)
        {
            return currentRobotPosition.GetAdjacentPosition();
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

        private bool IsPositionSafeForLavaBot(LavaBot lavaBot, Position newPosition, Topography topography)
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

        private bool IsPositionSafeForRobot(RobotBase robot, Position newPosition, Topography topography)
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

        private void UpdateRobotPosition(RobotBase robot, Position newPosition)
        {
            robots[robot] = newPosition;
        }
    }
}