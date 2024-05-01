using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public RobotResultBase FreezeRobotIfItHasntMovedForAWhile(RobotBase robot)
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
            switch (exoplanetService.ExoPlanet.Weather)
            {
                case Weather.WINDY:
                    return 15;

                case Weather.SNOWY:
                    return 20;

                case Weather.SUNNY:
                    return 30;

                default:
                    return 30;
            }
        }

        private void RobotFreeze(RobotBase robot)
        {
            exoplanetService.FreezeTracking.FreezeRobot(robot);
        }
    }
}