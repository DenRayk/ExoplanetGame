﻿using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Domain.Robot.Variants;

namespace ExoplanetGame.Application.Exoplanet
{
    internal class RobotPositionService : RobotPostionUseCase
    {
        private readonly ExoplanetService exoplanetService;

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

        public PositionResult GetRobotPosition(IRobot robot)
        {
            IExoPlanet exoPlanet = exoplanetService.ExoPlanet;
            if (exoplanetService.EnergyTracking.DoesRobotHaveEnoughEneryToAction(robot, RobotAction.GETPOSITION))
            {
                exoplanetService.HeatTracking.PerformAction(robot, RobotAction.GETPOSITION, exoPlanet.Topography);
                exoplanetService.EnergyTracking.ConsumeEnergy(robot, RobotAction.GETPOSITION);
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

        public void RemoveRobot(IRobot robot)
        {
            exoplanetService.ExoPlanet.RobotPositionManager.Robots.Remove(robot);
        }

        public bool IsPositionSafeForRobot(IRobot robot, Position newPosition, Topography topography,
            ref PositionResult positionResult)
        {
            if (newPosition == null) return false;

            if (!IsPositionInBounds(newPosition, topography))
            {
                positionResult.Message = "The position is out of bounds.\n";
                positionResult.Message += "Robot crashed.";
                positionResult.IsSuccess = false;
                positionResult.HasRobotSurvived = false;
                return false;
            }

            if (IsPositionLava(newPosition, topography) && robot is not LavaBot)
            {
                positionResult.Message = "The position is lava.\n";
                positionResult.Message += "Robot crashed.";
                positionResult.IsSuccess = false;
                positionResult.HasRobotSurvived = false;
                return false;
            }

            if (IsAnotherRobotAlreadyAtThisPosition(robot, newPosition))
            {
                positionResult.Message = "Another robot is already at this position.\n";
                positionResult.Message = "Robot crashed.";
                positionResult.IsSuccess = false;
                positionResult.HasRobotSurvived = false;
                return false;
            }

            return true;
        }

        public bool IsAnotherRobotAlreadyAtThisPosition(IRobot robot, Position position)
        {
            var robots = exoplanetService.ExoPlanet.RobotPositionManager.Robots;
            foreach (var otherRobot in robots.Keys)
            {
                if (otherRobot.Equals(robot)) continue;

                if (robots[otherRobot].X == position.X && robots[otherRobot].Y == position.Y)
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

        public void UpdateRobotPosition(IRobot robot, Position newPosition)
        {
            exoplanetService.ExoPlanet.RobotPositionManager.Robots[robot] = newPosition;
        }

        public Position WaterDrift(IRobot robot, Position position, Topography topography)
        {
            IExoPlanet exoplanet = exoplanetService.ExoPlanet;

            Ground currentRobotGround = exoplanet.Topography.GetMeasureAtPosition(position).Ground;

            if (currentRobotGround == Ground.WATER)
            {
                exoplanetService.HeatTracking.WaterCoolDown(robot);
            }

            if (robot is AquaBot)
            {
                return position;
            }

            while (position.Y < exoplanet.Topography.PlanetSize.Height - 1 && currentRobotGround == Ground.WATER)
            {
                position = new Position(position.X, position.Y + 1, robot.RobotInformation.Position?.Direction ?? Direction.NORTH);
                currentRobotGround = exoplanet.Topography.GetMeasureAtPosition(position).Ground;
            }

            while (position.Y == exoplanet.Topography.PlanetSize.Height - 1 && currentRobotGround == Ground.WATER)
            {
                position = new Position(position.X + 1, position.Y, robot.RobotInformation.Position?.Direction ?? Direction.NORTH);
                currentRobotGround = exoplanet.Topography.GetMeasureAtPosition(position).Ground;
            }

            return position;
        }
    }
}