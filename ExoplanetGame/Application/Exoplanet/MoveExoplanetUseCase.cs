using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Robot.RobotResults;
using ExoplanetGame.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Application.Exoplanet
{
    internal interface MoveExoplanetUseCase
    {
        public PositionResult MoveRobot(RobotBase robot);
    }
}