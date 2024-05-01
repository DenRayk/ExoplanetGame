using ExoplanetGame.Exoplanet.Environment;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Gaia : ExoPlanetBase
    {
        private Random random = new();

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

        public Gaia() : base(PlanetVariant.GAIA)
        {
            Weather = Weather.SUNNY;

            int randomVariant = random.Next(0, gaiaVariants.Count);
            Topography = new Topography(gaiaVariants[randomVariant]);

            RobotManager = new RobotManager(this);
        }

        public override void ChangeWeather()
        {
            int weatherChange = random.Next(1, 101);

            switch (weatherChange)
            {
                case <= 50:
                    Weather = Weather.SUNNY;
                    break;

                case <= 70:
                    Weather = Weather.RAINY;
                    break;

                case <= 80:
                    Weather = Weather.CLOUDY;
                    break;

                default:
                    Weather = Weather.FOGGY;
                    break;
            }
        }
    }
}