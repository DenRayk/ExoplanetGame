using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Exoplanet;
using ExoplanetGame.Exoplanet.Variants;

namespace ExoplanetGame.ControlCenter
{
    public class PlanetManager
    {
        private static List<IExoplanet> exoplanets = new List<IExoplanet>();

        public PlanetManager()
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
                    return exoplanets[0];
                case PlanetVariants.GAIA:
                    return exoplanets[1];
                case PlanetVariants.LAVARIA:
                    return exoplanets[2];
                case PlanetVariants.TERRA:
                    return exoplanets[3];
                case PlanetVariants.TROPICA:
                    return exoplanets[4];
                default:
                    return null;
            }
        }
    }
}
