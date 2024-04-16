using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Aquatica : ExoplanetBase
    {
        public Aquatica() : base(PlanetVariant.AQUATICA)
        {
            Weather = Weather.RAINY;

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

        public override void ChangeWeather()
        {
            Random random = new();
            int weatherChange = random.Next(1, 101);

            if (weatherChange <= 50)
            {
                Weather = Weather.RAINY;
            }
            else if (weatherChange <= 80)
            {
                Weather = Weather.CLOUDY;
            }
            else if (weatherChange <= 95)
            {
                Weather = Weather.FOGGY;
            }
            else
            {
                Weather = Weather.SUNNY;
            }
        }
    }
}