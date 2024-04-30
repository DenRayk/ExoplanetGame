using ExoplanetGame.Exoplanet.Variants;

namespace ExoplanetGame.Application.ControlCenter
{
    public interface SelectPlanetUseCase
    {
        void SelectPlanet(PlanetVariant planetVariant);
    }
}