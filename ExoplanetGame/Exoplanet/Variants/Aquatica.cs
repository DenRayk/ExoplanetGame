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

            //Casual level
            Topography = new Topography(new string[]
            {
                "GSSWPRSGGSSSSSSSSSWWW",
                "SPWWPSRSSMMMMMMMWWWWW",
                "SGWPMSRSSRMSSSSSSSWWW",
                "SSWWMGSRRGRRRRRRRRWWW",
                "RGSWWSSGRGSSSSSSWWWSS",
                "RRWWGGSGRWWWSSWWWSSSS",
                "SSSSSWWWWSSSWWWWWWWSS",
                "SSSWWSSSSSSSSWWWSSSSS"
            });
            robotManager = new RobotManager(this);
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