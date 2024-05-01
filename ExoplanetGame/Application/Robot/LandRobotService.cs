using System;
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
    internal class LandRobotService : LandRobotUseCase
    {
        private LandExoplanetService landRobotExoplanetService;
        private global::ExoplanetGame.ControlCenter.ControlCenter controlCenter;

        public LandRobotService(ExoplanetService exoplanetService)
        {
            controlCenter = global::ExoplanetGame.ControlCenter.ControlCenter.GetInstance();
            landRobotExoplanetService = new LandExoplanetService(exoplanetService);
        }

        public PositionResult LandRobot(RobotBase robotBase, Position landPosition)
        {
            PositionResult positionResult = landRobotExoplanetService.LandExoplanet(robotBase, landPosition);

            if (positionResult.IsSuccess)
            {
                robotBase.RobotInformation.HasLanded = true;
                robotBase.RobotInformation.Position = positionResult.Position;
                controlCenter.UpdateRobotPosition(robotBase, positionResult.Position);
            }

            return positionResult;
        }
    }
}