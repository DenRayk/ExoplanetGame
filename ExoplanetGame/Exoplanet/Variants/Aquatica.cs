using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

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

            if (weatherChange <= 50)
            {
                Weather = Weather.RAINY;
            }
            else if (weatherChange <= 80)
            {
                Weather = Weather.CLOUDY;
            }
            else if (weatherChange <= 95)
            {
                Weather = Weather.FOGGY;
            }
            else
            {
                Weather = Weather.SUNNY;
            }
        }
    }
}