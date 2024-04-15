using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Lavaria : ExoplanetBase
    {
        public Lavaria()
        {
            PlanetVariant = PlanetVariant.LAVARIA;

            Topography = new Topography(new string[]
            {
                "LLLLLLGGGGGLLLGGGGGGG",
                "LLGGGLLLLLLLLLGGGGLLL",
                "GGGGGLLLLLLLLLLLGGGLL",
                "LLLLLLLLGGGGGGGLLLGGL",
                "GGGGGGGFFFFFFFFLLLLFL",
                "FFFGGGGGGLLLFFFFFFFFF",
                "FFFFFFFFFGGGGGGGFFFFF"
            });
            robotManager = new RobotManager(Topography);
        }
    }
}