using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Exoplanet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExoplanetGameTest.Mocks.Planets
{
    internal class LavaPlanet : IExoPlanet
    {
        public LavaPlanet()
        {
            Topography = new Topography(new string[]
            {
                "RRRRRRRRRRRRRRRRR",
                "RLLLLLLLLLLLLLLLR",
                "RLLLLLLLLLLLLLLLR",
                "RLLLLLLLLLLLLLLLR",
                "RLLLLLLLLLLLLLLLR",
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
