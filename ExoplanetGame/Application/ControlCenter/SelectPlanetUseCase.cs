using ExoplanetGame.Domain.Exoplanet;

namespace ExoplanetGame.Application.ControlCenter
{
    public interface SelectPlanetUseCase
    {
        void SelectPlanet(ExoPlanetBase exoPlanetBase);
    }
}