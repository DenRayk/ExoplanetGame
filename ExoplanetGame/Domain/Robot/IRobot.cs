using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Robot.Variants;

namespace ExoplanetGame.Domain.Robot
{
    public interface IRobot
    {
        ExoPlanetBase ExoPlanet { get; }
        RobotInformation RobotInformation { get; }

        string GetLanderName();
    }
}