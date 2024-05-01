using ExoplanetGame.Exoplanet.Variants;

namespace ExoplanetGame.Exoplanet.Factory
{
    internal interface ExoPlanetBaseFactory
    {
        ExoPlanetBase CreateExoPlanet(PlanetVariant planetVariant);
    }
}