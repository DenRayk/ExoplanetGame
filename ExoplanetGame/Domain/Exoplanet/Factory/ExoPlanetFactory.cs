using ExoplanetGame.Domain.Exoplanet.Variants;

namespace ExoplanetGame.Domain.Exoplanet.Factory
{
    internal class ExoPlanetFactory : IExoPlanetBaseFactory
    {
        private static ExoPlanetFactory instance;

        public IExoPlanet CreateExoPlanet(PlanetVariant planetVariant)
        {
            switch (planetVariant)
            {
                case PlanetVariant.GAIA:
                    return new Gaia();

                case PlanetVariant.AQUATICA:
                    return new Aquatica();

                case PlanetVariant.FROSTFELL:
                    return new Frostfell();

                case PlanetVariant.LAVARIA:
                    return new Lavaria();

                case PlanetVariant.TERRA:
                    return new Terra();

                case PlanetVariant.TROPICA:
                    return new Tropica();

                default:
                    throw new ArgumentException("Invalid planet variant");
            }
        }

        public static ExoPlanetFactory GetInstance()
        {
            if (instance == null)
            {
                instance = new ExoPlanetFactory();
            }
            return instance;
        }
    }
}