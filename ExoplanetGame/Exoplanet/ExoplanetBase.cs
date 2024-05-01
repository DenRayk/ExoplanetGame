using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Exoplanet.Variants;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Exoplanet
{
    public class ExoPlanetBase
    {
        internal Weather Weather { get; set; }

        public RobotPositionManager RobotPositionManager { get; set; }
        public RobotStatusManager RobotStatusManager { get; }

        public Topography Topography { get; set; }
        public PlanetVariant PlanetVariant { get; set; }

        public ExoPlanetBase()
        {
            PlanetVariant = PlanetVariant.GAIA;
            Weather = Weather.SUNNY;
            RobotPositionManager = new RobotPositionManager(this);
            RobotStatusManager = new RobotStatusManager();
        }

        public ExoPlanetBase(PlanetVariant planetVariant)
        {
            PlanetVariant = planetVariant;
            Weather = Weather.SUNNY;
            RobotPositionManager = new RobotPositionManager(this);
            RobotStatusManager = new RobotStatusManager();
        }

        public virtual void ChangeWeather()
        {
            Random random = new Random();
            int weatherChange = random.Next(1, 101);

            if (weatherChange <= 40)
            {
                Weather = Weather.SUNNY;
            }
            else if (weatherChange <= 70)
            {
                Weather = Weather.CLOUDY;
            }
            else if (weatherChange <= 90)
            {
                Weather = Weather.RAINY;
            }
            else if (weatherChange <= 95)
            {
                Weather = Weather.WINDY;
            }
            else
            {
                Weather = Weather.FOGGY;
            }
        }
    }
}