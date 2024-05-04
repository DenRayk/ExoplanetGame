using ExoplanetGame.Domain.Exoplanet;

namespace ExoplanetGame.Application.ControlCenter
{
    public interface SelectPlanetUseCase
    {
        void SelectPlanet(IExoPlanet exoPlanetBase);
    }
}