using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGame.Domain.Exoplanet.Variants
{
    public class Tropica : IExoPlanet
    {
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

        public Tropica()
        {
            Weather = Weather.SUNNY;

            Random random = new();
            int randomVariant = random.Next(0, tropicaVariants.Count);
            Topography = new Topography(tropicaVariants[randomVariant]);

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