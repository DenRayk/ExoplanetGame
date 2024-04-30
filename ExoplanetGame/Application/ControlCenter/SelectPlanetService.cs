using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet.Factory;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;

namespace ExoplanetGame.Application.ControlCenter
{
    internal class SelectPlanetService : SelectPlanetUseCase
    {
        private global::ExoplanetGame.ControlCenter.ControlCenter controlCenter;
        private readonly IExoPlanetFactory exoPlanetFactory;

        public SelectPlanetService()
        {
            controlCenter = global::ExoplanetGame.ControlCenter.ControlCenter.GetInstance();
            exoPlanetFactory = ExoPlanetFactory.GetInstance();
        }

        public void SelectPlanet(PlanetVariant planetVariant)
        {
            IExoPlanet exoPlanet = exoPlanetFactory.CreateExoPlanet(planetVariant);
            controlCenter.exoPlanet = exoPlanet;
            controlCenter.PlanetMap = new PlanetMap(exoPlanet.Topography.PlanetSize);
        }
    }
}