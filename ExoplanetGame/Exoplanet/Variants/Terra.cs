using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Terra : ExoplanetBase
    {
        public Terra() : base(PlanetVariant.TERRA)
        {
            Weather = Weather.WINDY;
            //Intermediate level
            Topography = new Topography(new string[]
            {
                "GGLLRRSSWSRLLGR",
                "RGGLRRSRWSRRLLL",
                "RLLLGSSRWWSRRGR",
                "RLRRRRSGGWSSSGR",
                "RLLRRLRSSWWGSSW",
                "LLLRLLRSRSWGWWW",
                "RRRGRRRSRRWWWSS",
                "RRGSGGSSSRRWSSS"
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
                Weather = Weather.RAINY;
            }
            else if (weatherChange <= 75)
            {
                Weather = Weather.CLOUDY;
            }
            else
            {
                int windyOrFoggy = random.Next(1, 3);
                Weather = (windyOrFoggy == 1) ? Weather.WINDY : Weather.FOGGY;
            }
        }
    }
}