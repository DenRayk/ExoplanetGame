using ExoplanetGame.Domain.Exoplanet.Variants;

namespace ExoplanetGame.Domain.Exoplanet.Factory
{
    public class FrostfellPlanetFactory : ExoPlanetFactory
    {
        public override IExoPlanet CreateExoPlanet()
        {
            return new Frostfell();
        }
    }
}