using ExoplanetGame.Domain.Exoplanet.Variants;

namespace ExoplanetGame.Domain.Exoplanet.Factory
{
    internal class TerraPlanetFactory : ExoPlanetFactory
    {
        public override IExoPlanet CreateExoPlanet()
        {
            return new Terra();
        }
    }
}