using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Exoplanet.Variants
{
    public class Lavaria : ExoplanetBase
    {
        private Random random = new();

        private readonly List<string[]> lavariaVariants = new()
        {
            new string[]
            {
                "GLLLLLGGGGGLLLGGGGGGG",
                "LLGGGLLLLLLLLLGGGGLLL",
                "GGGGGLLLLLLLLLLLGGGLL",
                "LLLLLLLLGGGGGGGLLLGGL",
                "GGGLLGGRRRRRRRRLLLLRL",
                "RRRGGGGGGLLLRRRRRRRRR",
                "RRRRRRRRRGGGGGGGRRRRR"
            },

            new string[]
            {
                "GLGLGRRGLLLRGRGGLGRRG",
                "LLGLGRRRLLGRGRGGLLLRG",
                "LGGLGRRRLLRRLGGGGLLRR",
                "LGGLLGRGLRRRLGGGGLLRR",
                "LGGLLGRLLRRRLGGLGGLRR",
                "LLLLGGRLLLGRRGGLLGRRR",
                "GLLLGGRLLLGRRGGLLLLRR"
            },

            new string[]
            {
                "LLLLLLLLGGLLGRLLLGRLG",
                "GGGLLGGLGGLLGLLLLGRRG",
                "RRRGGGGLLLLGGRLLLGRRG",
                "RRRRRRRGLLLGGRGGLGRRG",
                "GRRRLLRGLLLRGRGGLLLRG",
                "LLLRLLLGLLGRGRGGGLLRR",
                "LLLRRLLGLLGRLGGGGLLRR"
            },

            new string[]
            {
                "LGGRGLLLGGRRLLLLLLLGG",
                "LLLRGLLLRGGGGGLLGGRRR",
                "GGLLGLLGRGLLLLGGGGGGL",
                "GGLLGLLGRLLLLLRLRRRRG",
                "LLLLLGGLLGLLGGLLLGGGG",
                "LLGGLGGLLGGRRRLLLGGGG",
                "GGGGLLLLGGGGGLLLLLLGG"
            },

            new string[]
            {
                "RRRLLLRLGGLGRRLGLLGRG",
                "LLLLGGRLGGLLGRLGLLGRL",
                "LLLLLRRLGGLLGRRRGRLLL",
                "LRRRLRRLLLLGGRRRGLLLL",
                "RRGGLGRGLLLGGRRRGRLLL",
                "RRRRLGRGLLLRGRLGGRGGL",
                "LRLRLGRGLLGRGRLRGRGGL"
            }
        };

        public Lavaria() : base(PlanetVariant.LAVARIA)
        {
            Weather = Weather.FOGGY;

            int randomVariant = random.Next(0, lavariaVariants.Count);
            Topography = new Topography(lavariaVariants[randomVariant]);

            robotManager = new RobotManager(this);
        }

        public override void ChangeWeather()
        {
            int weatherChange = random.Next(1, 101);

            if (weatherChange <= 25)
            {
                Weather = Weather.SUNNY;
            }
            else if (weatherChange <= 50)
            {
                Weather = Weather.FOGGY;
            }
            else if (weatherChange <= 75)
            {
                Weather = Weather.ASH_IN_THE_AIR;
            }
            else
            {
                int cloudyOrWindy = random.Next(1, 3);
                Weather = (cloudyOrWindy == 1) ? Weather.CLOUDY : Weather.WINDY;
            }
        }
    }
}