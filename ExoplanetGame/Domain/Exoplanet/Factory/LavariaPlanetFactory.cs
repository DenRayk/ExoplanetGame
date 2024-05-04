using ExoplanetGame.Domain.Exoplanet.Variants;

namespace ExoplanetGame.Domain.Exoplanet.Factory
{
    internal class LavariaPlanetFactory : ExoPlanetFactory
    {
        public override IExoPlanet CreateExoPlanet()
        {
            return new Lavaria();
        }
    }
}