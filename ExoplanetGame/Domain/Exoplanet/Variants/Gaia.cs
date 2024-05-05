using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGame.Domain.Exoplanet.Variants
{
    public class Gaia : IExoPlanet
    {
        private readonly List<string[]> gaiaVariants = new()
        {
            new string[]
            {
                "GSSWPRSGGL",
                "SPWWPSRRLL",
                "SGWPMSRLLR",
                "SSWWMGSRRG",
                "RGSWWSSGRG",
                "SRWWGGSGRR"
            },

            new string[]
            {
                "SWWWSWGSSS",
                "WWPWGWSPGS",
                "PPMMGGSWWW",
                "RSSGSGRRRR",
                "SRRSSSGRSS",
                "SRLRGGSSSG"
            },

            new string[]
            {
                "GSGSWSSSWW",
                "SPWWGGPSWW",
                "SGGPWPWWMM",
                "RRRGRSSSRG",
                "RSGRSSRRRR",
                "SRGSSGRLLG"
            },

            new string[]
            {
                "GWGSWWWSSS",
                "PPPGWWWWSW",
                "GPSWGWMPWM",
                "SRRSRSGGRR",
                "SRSSRSRGSS",
                "SGSSSGSRGG"
            },

            new string[]
            {
                "WWSSSWMPSS",
                "SSWWWWMPGS",
                "SRRRWMMWWW",
                "RRGGWGRRRR",
                "RRRRSSGRSS",
                "SRSRGGSSSG"
            }
        };

        public Gaia()
        {
            Weather = Weather.SUNNY;

            Random random = new();
            int randomVariant = random.Next(0, gaiaVariants.Count);
            Topography = new Topography(gaiaVariants[randomVariant]);

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
                <= 50 => Weather.SUNNY,
                <= 70 => Weather.RAINY,
                <= 80 => Weather.CLOUDY,
                _ => Weather.FOGGY
            };
        }
    }
}