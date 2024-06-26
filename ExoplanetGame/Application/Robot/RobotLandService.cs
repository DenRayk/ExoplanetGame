﻿using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot
{
    internal class RobotLandService : RobotLandUseCase
    {
        private readonly ExoplanetService exoplanetService;
        private readonly IRobotRepository robotRepository;

        public RobotLandService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
            robotRepository = RobotRepository.GetInstance();
        }

        public PositionResult LandRobot(IRobot robot, Position landPosition)
        {
            PositionResult positionResult = exoplanetService.LandOnExoplanetService.LandExoplanet(robot, landPosition);

            if (positionResult.IsSuccess)
            {
                robotRepository.MoveRobot(robot, positionResult.Position);
                robot.RobotInformation.HasLanded = true;
                robot.RobotInformation.Position = positionResult.Position;
            }

            return positionResult;
        }
    }
}