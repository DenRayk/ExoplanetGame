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
                "GSSWPRSGGL",
                "SPWWPSRRLL",
                "SGWPMSRLLR",
                "SSWWMGSRRG",
                "RGSWWSSGRG",
                "RRWWGGSGRR"
            });
            robotManager = new RobotManager(this);
        }
    }
}