using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Domain.Robot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Application.Exoplanet.PlanetEvents
{
    public interface PlanetEventsUseCase
    {
        RobotResultBase ExecutePlanetEvents(IRobot robot);
    }
}