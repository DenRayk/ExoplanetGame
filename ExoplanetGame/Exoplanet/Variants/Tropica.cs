using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Tropica : ExoPlanetBase
    {
        private static readonly int mysteriousAttackChance = 50;

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

            Random random = new();
            int randomVariant = random.Next(0, tropicaVariants.Count);
            Topography = new Topography(tropicaVariants[randomVariant]);

            RobotPositionManager = new RobotPositionManager(this);
        }

        public override void ChangeWeather()
        {
            Random random = new();
            int weatherChange = random.Next(1, 101);

            switch (weatherChange)
            {
                case <= 70:
                    Weather = Weather.RAINY;
                    break;

                case <= 80:
                    Weather = Weather.SUNNY;
                    break;

                case <= 90:
                    Weather = Weather.CLOUDY;
                    break;

                default:
                    Weather = Weather.FOGGY;
                    break;
            }
        }
    }
}