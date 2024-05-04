using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGameTest.Mocks
{
    internal class RockPlanet : IExoPlanet
    {
        public RockPlanet()
        {
            Topography = new Topography(new string[]
            {
                "RRRRRRRRRRRRRRRRR",
                "RRRRRRRRRRRRRRRRR",
                "RRRRRRRRRRRRRRRRR",
                "RRRRRRRRRRRRRRRRR",
                "RRRRRRRRRRRRRRRRR",
                "RRRRRRRRRRRRRRRRR",
            });
            RobotPositionManager = new RobotPositionManager();
            RobotStatusManager = new RobotStatusManager();
            Weather = Weather.SUNNY;
        }

        public Weather Weather { get; private set; }
        public RobotPositionManager RobotPositionManager { get; }
        public RobotStatusManager RobotStatusManager { get; }
        public Topography Topography { get; }

        public void ChangeWeather()
        {
            Weather = Weather.SUNNY;
        }
    }
}