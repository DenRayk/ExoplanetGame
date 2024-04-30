using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Variants;

namespace ExoplanetGame.ControlCenter
{
    public static class PlanetManager
    {
        private static List<ExoPlanetBase> exoplanets = new List<ExoPlanetBase>();

        public static ExoPlanetBase TargetPlanet { get; set; }

        public static void ScanForExoplanets()
        {
            Console.WriteLine("Scanning for exoplanets...\n");

            exoplanets.Add(new Aquatica());
            exoplanets.Add(new Gaia());
            exoplanets.Add(new Lavaria());
            exoplanets.Add(new Terra());
            exoplanets.Add(new Tropica());
            exoplanets.Add(new Frostfell());
        }

        public static ExoPlanetBase GetPlanet(PlanetVariant planet)
        {
            switch (planet)
            {
                case PlanetVariant.AQUATICA:
                    TargetPlanet = exoplanets[0];
                    return TargetPlanet;

                case PlanetVariant.GAIA:
                    TargetPlanet = exoplanets[1];
                    return TargetPlanet;

                case PlanetVariant.LAVARIA:
                    TargetPlanet = exoplanets[2];
                    return TargetPlanet;

                case PlanetVariant.TERRA:
                    TargetPlanet = exoplanets[3];
                    return TargetPlanet;

                case PlanetVariant.TROPICA:
                    TargetPlanet = exoplanets[4];
                    return TargetPlanet;

                case PlanetVariant.FROSTFELL:
                    TargetPlanet = exoplanets[5];
                    return TargetPlanet;

                default:
                    return null;
            }
        }

        public static ExoPlanetBase GetRandomPlanet()
        {
            Random random = new Random();
            int randomPlanet = random.Next(0, exoplanets.Count);

            TargetPlanet = exoplanets[randomPlanet];
            return TargetPlanet;
        }
    }
}