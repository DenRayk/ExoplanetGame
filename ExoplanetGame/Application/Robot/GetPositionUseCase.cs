using ExoplanetGame.Robot.RobotResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Application.Robot
{
    public interface GetPositionUseCase
    {
        PositionResult GetPosition(RobotBase robot);
    }
}