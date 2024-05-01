﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot
{
    internal class GetPositionService : GetPositionUseCase
    {
        private ExoplanetService exoplanetService;
        private RobotRepository robotRepository;

        public GetPositionService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
            robotRepository = RobotRepository.GetInstance();
        }

        public PositionResult GetPosition(RobotBase robot)
        {
            PositionResult positionResult = exoplanetService.RobotPostionsService.GetRobotPosition(robot);

            if (!positionResult.HasRobotSurvived)
            {
                exoplanetService.RobotPostionsService.RemoveRobot(robot);
                robotRepository.RemoveRobot(robot);
            }
            return exoplanetService.RobotPostionsService.GetRobotPosition(robot);
        }
    }
}