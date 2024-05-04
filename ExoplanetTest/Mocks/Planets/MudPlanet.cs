using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGameTest.Mocks.Planets
{
    internal class MudPlanet : IExoPlanet
    {
        public MudPlanet()
        {
            Topography = new Topography(new string[]
            {
                "RRRRRRRRRRRRRRRRR",
                "RMMMMMMMMMMMMMMMR",
                "RMMMMMMMMMMMMMMMR",
                "RMMMMMMMMMMMMMMMR",
                "RMMMMMMMMMMMMMMMR",
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
