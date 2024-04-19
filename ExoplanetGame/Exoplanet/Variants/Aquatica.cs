using ExoplanetGame.Exoplanet.Environment;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Aquatica : ExoplanetBase
    {
        private Random random = new();

        private readonly List<string[]> aquaticaVariants = new()
        {
            new string[]
            {
                "GSSWSRSGGSSSSSSPWWWWS",
                "SPWWPSRSSGMMWWWWWWWWW",
                "SGWWMSRGSRGSSSSSSSWWG",
                "SSWWWGSRRGRRRRRRRRWWG",
                "RGSWWSSGRGSSSSSSWWWGG",
                "RRWWGGSGRWWWSWWWWGGGS",
                "SSGGGWWWWSSWWWWWWWWSS",
                "SSSWWSSSSSSSSWWWGSSSS"
            },

            new string[]
            {
                "WWWSSMGGWWSSSGWGSSPWW",
                "SMWWWWPPSSSPWWWSSSSPP",
                "GWWWSSSSSWWMWPGWPSSSS",
                "GPMWWSSSWSWWWGGWWMWPS",
                "SSSWPWSSWSWSWGSWWWWPS",
                "GSSWWWWSSSWSSGSSPWPSS",
                "PWSSWSWWSMWSSPGSSWPSS",
                "SSSSWSSWWWWMSSSSSWPSG"
            },

            new string[]
            {
                "GWGSMWSSGMWSSGSMWGSPW",
                "WSSGRSWSGSMWSSGSWPGMW",
                "WWSSGRSWSGSWWGMWWGSWW",
                "SWWSWGMWSSGSWWSWGWWWS",
                "GSWGWWGWWMWGSSWWWGSWS",
                "GGSWWWGSWSSWGSWWSRGSS",
                "RRGSWSPGWSRWWPGWSGGGS",
                "RGSPGMWSPGRWGMSWRGWSS"
            },

            new string[]
            {
                "GSSSRRSSGSSRRRWSMGGWS",
                "SPGSGRSSSGRGGWSSWPPSS",
                "SWWWSWGSSMGRSWSSSSSSR",
                "WWPWWWGWSMSRSWPSSSSMS",
                "PPMMWGGWSMSRSSWSMSSMR",
                "RSSGSGWSSMSRSPWWWWSSG",
                "SRRSSSWSSMSRSPWWSWWSG",
                "GSGRGGWSPMSRSWWWSSWWG"
            },

            new string[]
            {
                "WWWSSMGGSSSSSWWGSSWWW",
                "RPWWWWSWSSMMSSWSSSWPS",
                "RPPWWPSWSMPSSMWSPWPSS",
                "RMMMWSSSSMWWWWWMWMRSP",
                "MMSSWMSSSMWPSSMPWRRSS",
                "MMMMWWWSWWWWSSSWWRGSS",
                "GMGMWPWGWSGGGSSGGGGSS",
                "GGGGMSPWWGGWWGSSSSSSG"
            }
        };

        public Aquatica() : base(PlanetVariant.AQUATICA)
        {
            Weather = Weather.RAINY;

            int randomVariant = random.Next(0, aquaticaVariants.Count);

            Topography = new Topography(aquaticaVariants[randomVariant]);

            robotManager = new RobotManager(this);
        }

        public override void ChangeWeather()
        {
            int weatherChange = random.Next(1, 101);

            switch (weatherChange)
            {
                case <= 40:
                    Weather = Weather.RAINY;
                    break;

                case <= 60:
                    Weather = Weather.CLOUDY;
                    break;

                case <= 80:
                    Weather = Weather.FOGGY;
                    break;

                default:
                    Weather = Weather.SUNNY;
                    break;
            }
        }
    }
}