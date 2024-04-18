using ExoplanetGame.Exoplanet.Environment;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Gaia : ExoplanetBase
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

            robotManager = new RobotManager(this);
        }

        public override void ChangeWeather()
        {
            int weatherChange = random.Next(1, 101);

            if (weatherChange <= 60)
            {
                Weather = Weather.SUNNY;
            }
            else if (weatherChange <= 90)
            {
                Weather = Weather.RAINY;
            }
            else if (weatherChange <= 95)
            {
                Weather = Weather.CLOUDY;
            }
            else
            {
                Weather = Weather.FOGGY;
            }
        }
    }
}