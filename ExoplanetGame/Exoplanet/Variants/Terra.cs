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

            switch (weatherChange)
            {
                case <= 30:
                    Weather = Weather.SUNNY;
                    break;

                case <= 50:
                    Weather = Weather.RAINY;
                    break;

                case <= 70:
                    Weather = Weather.CLOUDY;
                    break;

                case <= 90:
                    Weather = Weather.WINDY;
                    break;

                default:
                    Weather = Weather.FOGGY;
                    break;
            }
        }
    }
}