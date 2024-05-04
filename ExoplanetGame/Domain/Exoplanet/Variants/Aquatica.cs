using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGame.Domain.Exoplanet.Variants
{
    public class Aquatica : IExoPlanet
    {
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

        public Aquatica()
        {
            Weather = Weather.RAINY;

            Random random = new();
            int randomVariant = random.Next(0, aquaticaVariants.Count);

            Topography = new Topography(aquaticaVariants[randomVariant]);

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