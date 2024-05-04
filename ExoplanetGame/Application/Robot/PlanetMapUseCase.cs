using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Robot.Movement;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Application.Robot
{
    public interface PlanetMapUseCase
    {
        void UpdateMap(Position position, Ground ground);

        string GetPercentageOfExploredArea();
    }
}