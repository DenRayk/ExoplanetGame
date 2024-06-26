﻿using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet.PlanetEvents
{
    internal class FreezeService : FreezeUseCase
    {
        private ExoplanetService exoplanetService;

        public FreezeService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public RobotResultBase FreezeRobotIfItHasntMovedForAWhile(IRobot robot)
        {
            bool isRobotAlreadyFrozen = exoplanetService.FreezeTracking.IsFrozen(robot);
            if (isRobotAlreadyFrozen)
                return new RobotResultBase()
                {
                    HasRobotSurvived = true,
                    IsSuccess = false,
                    Message = "Robot is frozen and cannot move anymore.",
                };

            int resistanceTimeAgainstFreezing = GetFreezingTimeByWeatherConditions();

            DateTime lastMoveTime = exoplanetService.FreezeTracking.GetLastMove(robot);
            TimeSpan timeSpanSinceLastMove = DateTime.Now - lastMoveTime;
            bool isRobotFrozen = timeSpanSinceLastMove > TimeSpan.FromSeconds(resistanceTimeAgainstFreezing);

            if (isRobotFrozen)
            {
                RobotFreeze(robot);
                return new RobotResultBase()
                {
                    HasRobotSurvived = true,
                    IsSuccess = false,
                    Message = "Robot is frozen and cannot move anymore.",
                };
            }

            exoplanetService.FreezeTracking.UpdateLastMove(robot);

            return new RobotResultBase()
            {
                HasRobotSurvived = true,
                IsSuccess = true,
            };
        }

        private int GetFreezingTimeByWeatherConditions()
        {
            return exoplanetService.ExoPlanet.Weather switch
            {
                Weather.WINDY => 15,
                Weather.SNOWY => 20,
                Weather.SUNNY => 30,
                _ => 30
            };
        }

        private void RobotFreeze(IRobot robot)
        {
            exoplanetService.FreezeTracking.FreezeRobot(robot);
        }
    }
}