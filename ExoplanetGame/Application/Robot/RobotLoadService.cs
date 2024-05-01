using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot
{
    internal class RobotLoadService : RobotLoadUseCase
    {
        private ExoplanetService exoplanetService;
        private IRobotRepository robotRepository;

        public RobotLoadService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
            this.robotRepository = RobotRepository.GetInstance();
        }

        public LoadResult LoadEnergy(RobotBase robotBase, int seconds)
        {
            LoadResult loadResult = exoplanetService.LoadOnExoplanetService.LoadEnergy(robotBase, seconds);

            if (!loadResult.HasRobotSurvived)
            {
                exoplanetService.RobotPostionsService.RemoveRobot(robotBase);
                robotRepository.RemoveRobot(robotBase);
            }

            return loadResult;
        }
    }
}