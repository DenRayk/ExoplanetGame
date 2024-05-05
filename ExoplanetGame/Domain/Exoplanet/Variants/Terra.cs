using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGame.Domain.Exoplanet.Variants
{
    public class Terra : IExoPlanet
    {
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

        public Terra()
        {
            Weather = Weather.WINDY;

            Random random = new();
            int randomVariant = random.Next(0, terraVariants.Count);
            Topography = new Topography(terraVariants[randomVariant]);

            RobotPositionManager = new RobotPositionManager();
            RobotStatusManager = new RobotStatusManager();
        }

        public Weather Weather { get; private set; }
        public RobotPositionManager RobotPositionManager { get; }
        public RobotStatusManager RobotStatusManager { get; }
        public Topography Topography { get; }

        public void ChangeWeather()
        {
            Random random = new();

            int weatherChange = random.Next(1, 101);

            Weather = weatherChange switch
            {
                <= 30 => Weather.SUNNY,
                <= 50 => Weather.RAINY,
                <= 70 => Weather.CLOUDY,
                <= 90 => Weather.WINDY,
                _ => Weather.FOGGY
            };
        }
    }
}