using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Tropica : ExoplanetBase
    {
        public Tropica()
        {
            PlanetVariant = PlanetVariant.TROPICA;

            Topography = new Topography(new string[]
            {
                "GSSWPFSGGL",
                "SPWWPSFFLL",
                "SGWPMSFLLF",
                "SSWWMGSFFG",
                "FGSWWSSGFG",
                "FFWWGGSGFF"
            });
        }
    }
}