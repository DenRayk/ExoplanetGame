using ExoplanetGame.Domain.Exoplanet.Variants;

namespace ExoplanetGame.Domain.Exoplanet.Factory
{
    public abstract class ExoPlanetFactory
    {
        public abstract IExoPlanet CreateExoPlanet();
    }
}