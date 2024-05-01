﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot
{
    internal class RobotLandService : RobotLandUseCase
    {
        private LandExoplanetUseCase landRobotExoplanetService;
        private IRobotRepository robotRepository;

        public RobotLandService(ExoplanetService exoplanetService)
        {
            robotRepository = RobotRepository.GetInstance();
            landRobotExoplanetService = new LandExoplanetService(exoplanetService);
        }

        public PositionResult LandRobot(RobotBase robotBase, Position landPosition)
        {
            PositionResult positionResult = landRobotExoplanetService.LandExoplanet(robotBase, landPosition);

            if (positionResult.IsSuccess)
            {
                robotRepository.MoveRobot(robotBase, positionResult.Position);
                robotBase.RobotInformation.HasLanded = true;
                robotBase.RobotInformation.Position = positionResult.Position;
            }

            return positionResult;
        }
    }
}