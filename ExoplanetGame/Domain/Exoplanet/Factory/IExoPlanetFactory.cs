using ExoplanetGame.Domain.Exoplanet.Variants;

namespace ExoplanetGame.Domain.Exoplanet.Factory
{
    internal interface IExoPlanetBaseFactory
    {
        ExoPlanetBase CreateExoPlanet(PlanetVariant planetVariant);
    }
}