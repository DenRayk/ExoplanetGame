using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGameTest
{
    internal class MockedPlanet : IExoPlanet
    {
        public MockedPlanet()
        {
            Topography = new Topography(new string[]
            {
                "FFFFFFFF",
                "FFFFFFFF",
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