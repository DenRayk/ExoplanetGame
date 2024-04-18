using ExoplanetGame.Exoplanet.Environment;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Terra : ExoplanetBase
    {
        private Random random = new();

        private readonly List<string[]> terraVariants = new()
        {
            new string[]
            {
                "GGLLRRSPWSRLLGRR",
                "RGGLRRSPWPRRLLLL",
                "RLLLGRSRWWSRRGLR",
                "RLRRRRRGPWSSSGRR",
                "RLLRRLRSPWWGSSWW",
                "LLLLLLRSGPWGSWWW",
                "RRLLRRRSSGWWWWPP",
                "RRRRRRSSSSWWWPPP"
            },

            new string[]
            {
                "GRRRRLRRWWWPPGSS",
                "GGLLLLRRSSWWWPGG",
                "LGLRLLLRRRSSWWWW",
                "LLLRRLLRLRRSGGSW",
                "RRGRRLRRLLRSSSSW",
                "RRRRLLRRGRRGSWWW",
                "SSSRRRRSRRRRWWSP",
                "SSRGSSSSSSWWWPPP"
            },

            new string[]
            {
                "WSRLLGRSGGLLRRSS",
                "WSRRLRRSRGGLRRSS",
                "WWSRRRRWRLLLGRSR",
                "PWSSSGRWRLRRRRRG",
                "PWWGSSWWRLLRRLRS",
                "GPWGSWWPLLLLLLRS",
                "SGWSSWSPRRLLRRRS",
                "SGWWWWPPRRRRRRSS"
            },

            new string[]
            {
                "LLRSSSSWSRSSWWWW",
                "GRRGSWWWSRRSGWWW",
                "RRRRWWSPRSRSSSSW",
                "SSWWWPPPGRRGSWWW",
                "GRRRRLRRRRRRWWSP",
                "GGLLLLRRSSRRRWWP",
                "LGLRLLLRGRRRRRGG",
                "LLLRRLLRGGLLLRRG"
            },

            new string[]
            {
                "SSWWRRLRSSSWSRSS",
                "SWWWSSRLSWWWSRRS",
                "SWSWPSRLWWSPRSRS",
                "WWPPWGRRWPPPGRRG",
                "SRRRWWSGRLRRRRRR",
                "RRGGWSRGLLRRSSRR",
                "RRRRWRRLLLLRGRRR",
                "SRSRWRRLRLLRGGLL"
            }
        };

        public Terra() : base(PlanetVariant.TERRA)
        {
            Weather = Weather.WINDY;

            int randomVariant = random.Next(0, terraVariants.Count);
            Topography = new Topography(terraVariants[randomVariant]);

            robotManager = new RobotManager(this);
        }

        public override void ChangeWeather()
        {
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