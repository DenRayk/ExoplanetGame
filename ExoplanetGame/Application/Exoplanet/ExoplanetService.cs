using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Factory;
using ExoplanetGame.Exoplanet.Variants;

namespace ExoplanetGame.Application.Exoplanet
{
    public class ExoplanetService
    {
        private IExoPlanetFactory exoPlanetFactory;

        public ExoplanetService()
        {
            exoPlanetFactory = ExoPlanetFactory.GetInstance();
        }

        public IExoPlanet ExoPlanet { get; private set; }

        public void CreateExoPlanet(PlanetVariant planetVariant)
        {
            ExoPlanet = exoPlanetFactory.CreateExoPlanet(planetVariant);
        }
    }
}