using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Robot.Factory;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Application.ControlCenter
{
    internal class AddRobotService : AddRobotUseCase
    {
        private global::ExoplanetGame.ControlCenter.ControlCenter controlCenter;
        private RobotFactory robotFactory;

        public AddRobotService()
        {
            controlCenter = global::ExoplanetGame.ControlCenter.ControlCenter.GetInstance();
            robotFactory = RobotFactory.GetInstance();
        }

        public void AddRobot(RobotVariant robotVariant)
        {
        }
    }
}