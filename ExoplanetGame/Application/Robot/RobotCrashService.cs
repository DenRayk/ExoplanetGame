using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot
{
    internal class RobotCrashService : RobotCrashUseCase
    {
        private IRobotRepository robotRepository;
        private ExoplanetService exoplanetService;

        public RobotCrashService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
            robotRepository = RobotRepository.GetInstance();
        }

        public RobotResultBase Crash(RobotBase robot)
        {
            robotRepository.RemoveRobot(robot);
            exoplanetService.RobotPostionsService.RemoveRobot(robot);
            return new RobotResultBase { IsSuccess = true, HasRobotSurvived = false };
        }
    }
}