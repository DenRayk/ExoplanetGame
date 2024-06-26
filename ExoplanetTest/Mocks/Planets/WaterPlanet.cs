﻿using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGameTest.Mocks.Planets
{
    internal class WaterPlanet : IExoPlanet
    {
        public WaterPlanet()
        {
            Topography = new Topography(new string[]
            {
                "RRRRRRRRRRRRRRRRR",
                "RWWWWWWWWWWWWWWWR",
                "RWWWWWWWWWWWWWWWR",
                "RWWWWWWWWWWWWWWWR",
                "RWWWWWWWWWWWWWWWR",
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