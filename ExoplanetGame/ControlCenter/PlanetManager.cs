using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Variants;

namespace ExoplanetGame.ControlCenter
{
    public static class PlanetManager
    {
        private static List<IExoplanet> exoplanets = new List<IExoplanet>();

        public static IExoplanet TargetPlanet { get; set; }

        public static void ScanForExoplanets()
        {
            Console.WriteLine("Scanning for exoplanets...");

            exoplanets.Add(new Aquatica());
            exoplanets.Add(new Gaia());
            exoplanets.Add(new Lavaria());
            exoplanets.Add(new Terra());
            exoplanets.Add(new Tropica());
        }

        public static IExoplanet GetPlanet(PlanetVariants planet)
        {
            switch (planet)
            {
                case PlanetVariants.AQUATICA:
                    TargetPlanet = exoplanets[0];
                    return TargetPlanet;
                case PlanetVariants.GAIA:
                    TargetPlanet = exoplanets[1];
                    return TargetPlanet;
                case PlanetVariants.LAVARIA:
                    TargetPlanet = exoplanets[2];
                    return TargetPlanet;
                case PlanetVariants.TERRA:
                    TargetPlanet = exoplanets[3];
                    return TargetPlanet;
                case PlanetVariants.TROPICA:
                    TargetPlanet = exoplanets[4];
                    return TargetPlanet;
                default:
                    return null;
            }
        }
    }
}
