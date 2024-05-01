using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet
{
    public interface LandOnExoplanetUseCase
    {
        PositionResult LandExoplanet(RobotBase robot, Position landPosition);
    }
}