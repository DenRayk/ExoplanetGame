using ExoplanetGame.Exoplanet.Variants;

namespace ExoplanetGame.Exoplanet.Factory
{
    internal class ExoPlanetFactory : ExoPlanetBaseFactory
    {
        private static ExoPlanetFactory instance;

        public ExoPlanetBase CreateExoPlanet(PlanetVariant planetVariant)
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