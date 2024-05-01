using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Application.Exoplanet.PlanetEvents
{
    public interface FreezeUseCase
    {
        RobotResultBase FreezeRobotIfItHasntMovedForAWhile(RobotBase robot);
    }
}