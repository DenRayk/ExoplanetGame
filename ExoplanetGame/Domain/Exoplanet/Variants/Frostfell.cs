using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGame.Domain.Exoplanet.Variants
{
    public class Frostfell : IExoPlanet
    {
        private readonly List<string[]> frostfellVariants = new()
        {
            new string[]
            {
                "RRNWWWIIIIRNIIWWWIIIN",
                "NNNNWWIINNRRIIIWWIINN",
                "WNNNWWWIIINRNNNIWWWIN",
                "WNNNNWWWIINNNNNIIWWIN",
                "WNNWWWWWWIINNNNIIWIIR",
                "WWWNNIIIWWWWIIINNNNRR",
                "WWWWNNNNNIIWWWWWIIRRR",
                "WWWNNNNIIIIIIIINNNNRR"
            },

            new string[]
            {
                "RNWWWWWWINIIWWNIWWWII",
                "RNNNNWWWINIIIWIIIIWWW",
                "RRNNNWWWRRNNIWIIIIWWI",
                "RRNNWNWNNRRNNWWIINWII",
                "RWWNWNNNIIRNNIWINNWNI",
                "WWWWWINNIIRNNIWINIWII",
                "IIWWWINNWINNNIWINWWII",
                "IIIWWINIWWIIINWNWWNII"
            },

            new string[]
            {
                "WWWWNNNNNNRRWWIIINIII",
                "WWWNNNNIWNNNWWWIINIIN",
                "IIRNIIWWWNNNNWWWINNNN",
                "NNRRIIIWWNNWWWWWWWNNN",
                "IINRRRNIWWWNNIIINWWNI",
                "IINNNNNIWWWWNNNNNNNNI",
                "WIINNNNIWWWNNNNINIINI",
                "WWWWIIINIIRNIIWWNIINI"
            },

            new string[]
            {
                "WIINNNNIIINRRRNIIIIII",
                "WWWWIIINIINNNNNIIIIII",
                "NIIWWWWWWIINNNNIIIWWI",
                "IIIIIIINWWWWIIINIWWNI",
                "WIRRNNNWNIIWWWWWWIINI",
                "WIINNIWWIRIIIIINNNRNN",
                "WWWWWWWNWRRINNNWNRRNN",
                "IWWINIIIWIINNIWWNNNNN"
            },

            new string[]
            {
                "NNNNNINIIIWWNIWWNNWNR",
                "NNRRNINWIIIWIRRIIWWNN",
                "NRRRRNWWNNIWIIRIIINRR",
                "RWWWRNNNRNNWWIINIINNN",
                "WWIIRNRNRNNIWINNWIINN",
                "WWWWRRRNRNNIWINIWWWWI",
                "RWRWRNNNNNNIWINWNIIWW",
                "RRRRNNNNIIINWNWWIRIII"
            }
        };

        public Frostfell()
        {
            Weather = Weather.SNOWY;

            Random random = new();
            int randomVariant = random.Next(0, frostfellVariants.Count);
            Topography = new Topography(frostfellVariants[randomVariant]);

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
                <= 50 => Weather.SNOWY,
                <= 75 => Weather.WINDY,
                _ => Weather.SUNNY
            };
        }
    }
}