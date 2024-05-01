﻿using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.Variants;

namespace ExoplanetGame.Application.Exoplanet.StatusTracking
{
    internal class RobotStuckTrackingService : RobotStuckTrackingUseCase
    {
        private ExoplanetService exoplanetService;
        private Random random = new();

        public RobotStuckTrackingService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public void CheckIfRobotGetsStuck(RobotBase robot, Topography topography, Position position)
        {
            if (robot.RobotVariant == RobotVariant.MUD)
                return;

            Ground newGround = topography.GetMeasureAtPosition(position).Ground;

            if (newGround == Ground.MUD || newGround == Ground.PLANT)
            {
                RobotGetStuckRandomly(robot);
            }
        }

        public void RobotGetStuckRandomly(RobotBase robot)
        {
            if (random.Next(0, 100) < 30)
            {
                exoplanetService.ExoPlanet.RobotStatusManager.RobotsStuck[robot] = true;
            }
        }

        public bool IsRobotStuck(RobotBase robot)
        {
            return exoplanetService.ExoPlanet.RobotStatusManager.RobotsStuck.ContainsKey(robot) && exoplanetService.ExoPlanet.RobotStatusManager.RobotsStuck[robot];
        }

        public void UnstuckRobot(RobotBase robot)
        {
            exoplanetService.ExoPlanet.RobotStatusManager.RobotsStuck[robot] = false;
        }
    }
}