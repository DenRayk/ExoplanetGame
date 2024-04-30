using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Variants;

namespace ExoplanetGame.Exoplanet.Factory
{
    internal interface IExoPlanetFactory
    {
        IExoPlanet CreateExoPlanet(PlanetVariant planetVariant);
    }
}