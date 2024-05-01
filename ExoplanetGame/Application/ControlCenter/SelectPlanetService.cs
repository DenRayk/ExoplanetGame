using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Factory;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;

namespace ExoplanetGame.Application.ControlCenter
{
    internal class SelectPlanetService : SelectPlanetUseCase
    {
        private global::ExoplanetGame.ControlCenter.ControlCenter controlCenter;

        public SelectPlanetService()
        {
            controlCenter = global::ExoplanetGame.ControlCenter.ControlCenter.GetInstance();
        }

        public void SelectPlanet(IExoPlanet exoPlanet)
        {
            controlCenter.exoPlanet = exoPlanet;
            controlCenter.PlanetMap = new PlanetMap(exoPlanet.Topography.PlanetSize);
        }
    }
}