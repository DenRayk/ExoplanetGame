using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Exoplanet;

namespace ExoplanetGame.Application.ControlCenter
{
    internal class SelectPlanetService : SelectPlanetUseCase
    {
        private Domain.ControlCenter.ControlCenter controlCenter;

        public SelectPlanetService()
        {
            controlCenter = Domain.ControlCenter.ControlCenter.GetInstance();
        }

        public void SelectPlanet(IExoPlanet exoPlanet)
        {
            controlCenter.exoPlanet = exoPlanet;
            controlCenter.PlanetMap = new PlanetMap(exoPlanet.Topography.PlanetSize);
        }
    }
}