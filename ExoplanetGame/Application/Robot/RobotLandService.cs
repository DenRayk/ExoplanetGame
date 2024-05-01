using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Robot
{
    internal class RobotLandService : RobotLandUseCase
    {
        private ExoplanetService exoplanetService;
        private IRobotRepository robotRepository;

        public RobotLandService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
            robotRepository = RobotRepository.GetInstance();
        }

        public PositionResult LandRobot(RobotBase robotBase, Position landPosition)
        {
            PositionResult positionResult = exoplanetService.LandOnExoplanetService.LandExoplanet(robotBase, landPosition);

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