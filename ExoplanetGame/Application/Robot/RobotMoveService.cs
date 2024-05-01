using System;
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
    internal class RobotMoveService : RobotMoveUseCase
    {
        private ExoplanetService exoplanetService;
        private IRobotRepository robotRepository;
        private MoveExoplanetUseCase moveExoplanetService;

        public RobotMoveService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
            robotRepository = RobotRepository.GetInstance();
            moveExoplanetService = new MoveExoplanetService(exoplanetService);
        }

        public PositionResult Move(RobotBase robot)
        {
            PositionResult positionResult = moveExoplanetService.MoveRobot(robot);

            if (positionResult.IsSuccess)
            {
                robotRepository.MoveRobot(robot, positionResult.Position);
                return positionResult;
            }

            if (!positionResult.HasRobotSurvived)
            {
                exoplanetService.RobotPostionsService.RemoveRobot(robot);
                robotRepository.RemoveRobot(robot);
            }

            return positionResult;
        }
    }
}