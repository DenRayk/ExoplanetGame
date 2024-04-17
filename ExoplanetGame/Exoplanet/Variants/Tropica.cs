using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Tropica : ExoplanetBase
    {
        private Random random = new();

        private readonly List<string[]> tropicaVariants = new()
        {
            new string[]
            {
                "GGWWPMWPGGGPWWWWRRGGWSPGGPSSSG",
                "GGWWSMWWWPMMPGGSWPWPWMGPGMMPSS",
                "GMMWSPPRWWWWMGGSSWMMWSMGMWWPRM",
                "GPMWSSRPGMMWWRRWSPMWWWSPPPWRPM",
                "SGPWGSMMMMPMWWPWWWWWSWWPPGWWGM",
                "SSWWGSPPGGMWGWWWWWWWSMWSMGPWMM",
                "GGSWGMMPGGGMPGSPRGWWSSWSSGMWMP",
                "SSPWWPMMGGGPPPMWMGWPSMWPSPMWWM",
                "MSPWWRMGGPMPPSMWGGWPSMWSSWPMWW",
                "RRMRWRPGGPWRRGSMGGWPSMSRRWGGRR"
            },

            new string[]
            {
                "GGGGSSGSMRGGWWPMWPGGGGGGSSGSMR",
                "GGMPGSGSSRGGWWSMWWWPGGMPGSGSSR",
                "WWMMPWSPPMGMMWSPPRWWWWMMPWSPPM",
                "WWWWWWWWWRGPMWSSRPGMWWWWWWWWWR",
                "PSSSGGGWWWSGPWGSMMMMPSSSGGGWWW",
                "MMPSSSMPRRSSWWGSPPGGMMPSSSMPRR",
                "WWPRMPMMMPGGSWGMMPGGWWPRMPMMMP",
                "PWRPMPPMGGSSPWWPMMGGPWRPMPPMGG",
                "GWWGMGGGGGMSPWWRMGGPGWWGMGGGGG",
                "GPWMMGGGPPRRMRWRPGGPGPWMMGGGPP"
            },

            new string[]
            {
                "GPWWWWRRGGGMWMPMGGMWGPWWWWRRGG",
                "MMPGGSWPWPPMWWMWMPPRMMPGGSWPWP",
                "WWMGGSSWMMWPMWWGPPPRWWMGGSSWMM",
                "MWWRRWSPMWWGGRWWGPSGMWWRRWSPMW",
                "PMWWPWWWWWWGGRPWSMMSPMWWPWWWWW",
                "MWGWWWWWWWWSSWWWPWWMMWGWWWWWWW",
                "GMPGSPRGWWRWSSWWRMGGGMPGSPRGWW",
                "GPPPMWMGWPRPWPWWGGGGGPPPMWMGWP",
                "MPPSMWGGWPGWMMWWWWWWMPPSMWGGWP",
                "WRRGSMGGWPGPMWWWWPPPWRRGSMGGWP"
            },

            new string[]
            {
                "WWWWSSSSSSSWWGSPPGGGWWWWSSSSSS",
                "SMSWWMSMMGGSWGMMPGGSSMSWWMSMMM",
                "PGMSWWWWWSSPWWPMMGGMPGMSWWWWWS",
                "GPGPPSSPSMSPWWRMGGPMGPGPPSSPSR",
                "GGMPPMSSSRRMRWRPGGPMGGMPPMSSSR",
                "PMWPGGGPWGGGGSSGSMRMPMWPGGGPWW",
                "SMWWWPMMPGGMPGSGSSRPSMWWWPMMPG",
                "SPPRWWWWMWWMMPWSPPMMSPPRWWWWMG",
                "SSRPGMMWWWWWWWWWWWRWSSRPGMMWWR",
                "GSMMMMPMWPSSSGGGWWWWGSMMMMPMWM"
            },

            new string[]
            {
                "WGGRWWGPSGWWWRRGGGMWWWWWSSSSSS",
                "WGGRPWSMMSGGSWPWPPMWSMSWWMSMMM",
                "WSSWWWPWWMGGSSWMMWPMPGMSWWWWWS",
                "RWSSWWRMGGRRWSPMWWGGGPGPPSSPSR",
                "PPWPWWGGGGWPWWWWWWGGGGMPPMSSSR",
                "PRRRWWWWWWWWWWWWWWSSPMWPGGGPWW",
                "RRGGWWWPPPGSPRGWWRWSSMWWWPMMPG",
                "RRRRWWRRGGPMWMGWPRPWSPPRWWWWMG",
                "PRPRMSWPWPSMWGGWPGWMSSRPGMMWWR",
                "WPPPMSSWMMGSMGGWPGPMGSMMMMPMWR"
            }
        };

        public Tropica() : base(PlanetVariant.TROPICA)
        {
            Weather = Weather.SUNNY;

            int randomVariant = random.Next(0, tropicaVariants.Count);
            Topography = new Topography(tropicaVariants[randomVariant]);

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