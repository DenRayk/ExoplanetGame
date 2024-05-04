using ExoplanetGame.Domain.Exoplanet.Variants;

namespace ExoplanetGame.Domain.Exoplanet.Factory
{
    internal class TropicaPlanetFactory : ExoPlanetFactory
    {
        public override IExoPlanet CreateExoPlanet()
        {
            return new Tropica();
        }
    }
}