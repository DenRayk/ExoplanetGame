using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Terra : ExoplanetBase
    {
        public Terra()
        {
            PlanetVariant = PlanetVariant.TERRA;
            Topography = new Topography(new string[]
            {
                "GSSWPFSGGL",
                "SPWWPSFFLL",
                "SGWPMSFLLF",
                "SSWWMGSFFG",
                "FGSWWSSGFG",
                "FFWWGGSGFF"
            });
            robotManager = new RobotManager(Topography);
        }
    }
}