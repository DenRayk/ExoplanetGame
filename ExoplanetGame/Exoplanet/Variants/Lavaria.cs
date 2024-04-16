using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Lavaria : ExoplanetBase
    {
        public Lavaria() : base(PlanetVariant.LAVARIA)
        {
            Weather = Weather.FOGGY;

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
            robotManager = new RobotManager(this);
        }

        public override void ChangeWeather()
        {
            Random random = new Random();
            int weatherChange = random.Next(1, 101);

            if (weatherChange <= 25)
            {
                Weather = Weather.SUNNY;
            }
            else if (weatherChange <= 50)
            {
                Weather = Weather.FOGGY;
            }
            else if (weatherChange <= 75)
            {
                Weather = Weather.ASH_IN_THE_AIR;
            }
            else
            {
                int cloudyOrWindy = random.Next(1, 3);
                Weather = (cloudyOrWindy == 1) ? Weather.CLOUDY : Weather.WINDY;
            }
        }
    }
}