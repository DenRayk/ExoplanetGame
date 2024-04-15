using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Aquatica : ExoplanetBase
    {
        public Aquatica()
        {
            PlanetVariant = PlanetVariant.AQUATICA;

            Topography = new Topography(new string[]
            {
                "GSSWPFSGGSSSSSSSSSWWW",
                "SPWWPSFFSSMMMMMMMWWWW",
                "SGWPMSFSSFMSSSSSSSWWW",
                "SSWWMGSFFGFFFFFFFFWWW",
                "FGSWWSSGFGSSSSSSSSWWW",
                "FFWWGGSGFFSSSSSSSSWWW",
                "SSSSSSSSSSSSSSSSSSWWW",
                "SSSSSSSSSSSSSSSSSSWWW"
            });
        }
    }
}