using ExoplanetGame.Exoplanet.Variants;

namespace ExoplanetGame.Application.ControlCenter
{
    internal interface SelectPlanetUseCase
    {
        void SelectPlanet(PlanetVariant planetVariant);
    }
}