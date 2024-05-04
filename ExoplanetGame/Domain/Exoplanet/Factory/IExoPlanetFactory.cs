using ExoplanetGame.Domain.Exoplanet.Variants;

namespace ExoplanetGame.Domain.Exoplanet.Factory
{
    internal interface IExoPlanetBaseFactory
    {
        IExoPlanet CreateExoPlanet(PlanetVariant planetVariant);
    }
}