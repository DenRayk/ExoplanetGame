using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Gaia : ExoplanetBase
    {
        public Gaia() : base(PlanetVariant.GAIA)
        {
            Weather = Weather.SUNNY;

            //Beginner level
            Topography = new Topography(new string[]
            {
                "GSSWPFSGGL",
                "SPWWPSFFLL",
                "SGWPMSFLLF",
                "SSWWMGSFFG",
                "FGSWWSSGFG",
                "FFWWGGSGFF"
            });
            robotManager = new RobotManager(this);
        }
    }
}