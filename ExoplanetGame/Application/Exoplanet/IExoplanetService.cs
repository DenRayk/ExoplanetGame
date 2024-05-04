using ExoplanetGame.Domain.Exoplanet.Variants;
using ExoplanetGame.Domain.Exoplanet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGame.Application.Exoplanet
{
    public interface IExoplanetService
    {
        IExoPlanet ExoPlanet { get; set; }

        void CreateExoPlanet(PlanetVariant planetVariant);
    }
}