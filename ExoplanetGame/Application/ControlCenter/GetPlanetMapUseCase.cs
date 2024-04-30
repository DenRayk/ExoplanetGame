using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.ControlCenter;

namespace ExoplanetGame.Application.ControlCenter
{
    internal interface GetPlanetMapUseCase
    {
        PlanetMap GetPlanetMap();
    }
}