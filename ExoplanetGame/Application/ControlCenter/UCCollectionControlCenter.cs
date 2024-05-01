using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application.Exoplanet;

namespace ExoplanetGame.Application.ControlCenter
{
    public class UCCollectionControlCenter
    {
        public UCCollectionControlCenter()
        {
            this.SelectPlanetUseCase = new SelectPlanetService();
            this.AddRobotUseCase = new AddRobotService();
            this.GetRobotsService = new GetRobotsService();
            this.GetPlanetMapUseCase = new GetPlanetMapService();
            this.GetCurrentWeatherUseCase = new GetCurrentWeatherService();
        }

        public SelectPlanetUseCase SelectPlanetUseCase { get; }
        public AddRobotUseCase AddRobotUseCase { get; }
        public GetRobotsService GetRobotsService { get; }

        public GetCurrentWeatherUseCase GetCurrentWeatherUseCase { get; }

        public GetPlanetMapUseCase GetPlanetMapUseCase { get; }
    }
}