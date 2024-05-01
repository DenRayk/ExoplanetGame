using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Domain.Exoplanet;
using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGame.Application.Exoplanet.PlanetEvents
{
    internal class ChangeWeatherService : ChangeWeatherUseCase
    {
        private ExoplanetService exoplanetService;

        public ChangeWeatherService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
        }

        public void ChangeWeather()
        {
            exoplanetService.ExoPlanet.ChangeWeather();
        }
    }
}