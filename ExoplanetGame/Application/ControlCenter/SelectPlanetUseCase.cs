using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Variants;

namespace ExoplanetGame.Application.ControlCenter
{
    public interface SelectPlanetUseCase
    {
        void SelectPlanet(IExoPlanet exoPlanetBase);
    }
}