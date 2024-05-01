using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGame.Domain.Exoplanet.Variants
{
    public class Lavaria : ExoPlanetBase
    {
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

            Random random = new();
            int randomVariant = random.Next(0, lavariaVariants.Count);
            Topography = new Topography(lavariaVariants[randomVariant]);

            RobotPositionManager = new RobotPositionManager(this);
        }

        public override void ChangeWeather()
        {
            Random random = new();
            int weatherChange = random.Next(1, 101);

            switch (weatherChange)
            {
                case <= 25:
                    Weather = Weather.SUNNY;
                    break;

                case <= 50:
                    Weather = Weather.FOGGY;
                    break;

                case <= 75:
                    Weather = Weather.ASH_IN_THE_AIR;
                    break;

                case <= 85:
                    Weather = Weather.WINDY;
                    break;

                default:
                    Weather = Weather.CLOUDY;
                    break;
            }
        }
    }
}