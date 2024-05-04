using ExoplanetGame.Domain.Exoplanet.Variants;

namespace ExoplanetGame.Domain.Exoplanet.Factory
{
    internal class GaiaPlanetFactory : ExoPlanetFactory
    {
        public override IExoPlanet CreateExoPlanet()
        {
            return new Gaia();
        }
    }
}