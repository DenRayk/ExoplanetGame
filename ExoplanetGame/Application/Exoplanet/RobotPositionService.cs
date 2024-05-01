using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class RobotPositionService : RobotPostionUseCase
    {
        private ExoplanetService exoplanetService;

        public RobotPositionService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
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

        public PositionResult GetRobotPosition(RobotBase robot)
        {
            ExoPlanetBase exoPlanet = exoplanetService.ExoPlanet;
            if (exoplanetService.EnergyTrackingUseCase.DoesRobotHaveEnoughEneryToAction(robot, RobotAction.GETPOSITION))
            {
                exoplanetService.HeatTrackingUseCase.PerformAction(robot, RobotAction.GETPOSITION, exoPlanet.Topography);
                exoplanetService.EnergyTrackingUseCase.ConsumeEnergy(robot, RobotAction.GETPOSITION);
                return new PositionResult
                {
                    Position = exoPlanet.RobotPositionManager.Robots[robot],
                    IsSuccess = true,
                    HasRobotSurvived = true
                };
            }

            return new PositionResult
            {
                IsSuccess = false,
                HasRobotSurvived = true,
                Message = "The robot doesn't have enough energy to get its position."
            };
        }

        public void RemoveRobot(RobotBase robot)
        {
            exoplanetService.ExoPlanet.RobotPositionManager.Robots.Remove(robot);
        }

        public bool IsPositionSafeForRobot(RobotBase robot, Position newPosition, Topography topography,
            ref PositionResult positionResult)
        {
            if (newPosition == null) return false;

            if (!IsPositionInBounds(newPosition, topography))
            {
                positionResult.Message = "The position is out of bounds.";
                positionResult.IsSuccess = false;
                positionResult.HasRobotSurvived = false;
                return false;
            }

            if (IsPositionLava(newPosition, topography) && robot.RobotVariant != RobotVariant.LAVA)
            {
                positionResult.Message = "The position is lava.";
                positionResult.IsSuccess = false;
                positionResult.HasRobotSurvived = false;
                return false;
            }

            if (IsAnotherRobotAlreadyAtThisPosition(robot, newPosition))
            {
                positionResult.Message = "Another robot is already at this position.";
                positionResult.IsSuccess = false;
                positionResult.HasRobotSurvived = false;
                return false;
            }

            return true;
        }

        private bool IsAnotherRobotAlreadyAtThisPosition(RobotBase robotBase, Position position)
        {
            var robots = exoplanetService.ExoPlanet.RobotPositionManager.Robots;
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

        private bool IsPositionLava(Position position, Topography topography)
        {
            return topography.GetMeasureAtPosition(position).Ground == Ground.LAVA;
        }

        public void UpdateRobotPosition(RobotBase robot, Position newPosition)
        {
            throw new NotImplementedException();
        }

        public Position WaterDrift(RobotBase robot, Position position, Topography topography)
        {
            ExoPlanetBase exoplanet = exoplanetService.ExoPlanet;
            exoplanetService.HeatTrackingUseCase.WaterCoolDown(robot);

            if (robot.RobotVariant == RobotVariant.AQUA)
            {
                return position;
            }

            while (position.Y < exoplanet.Topography.PlanetSize.Height - 1 && exoplanet.Topography.GetMeasureAtPosition(position).Ground == Ground.WATER)
            {
                position = new Position(position.X, position.Y + 1);
            }

            while (position.Y == exoplanet.Topography.PlanetSize.Height - 1 && exoplanet.Topography.GetMeasureAtPosition(position).Ground == Ground.WATER)
            {
                position = new Position(position.X + 1, position.Y);
            }

            return position;
        }
    }
}