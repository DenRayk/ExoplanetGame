using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Exoplanet.Environment;

namespace ExoplanetGame.Application.ControlCenter
{
    internal class GetCurrentWeatherService : GetCurrentWeatherUseCase
    {
        private global::ExoplanetGame.ControlCenter.ControlCenter controlCenter;

        public GetCurrentWeatherService()
        {
            this.controlCenter = global::ExoplanetGame.ControlCenter.ControlCenter.GetInstance();
        }

        public Weather GetCurrentWeather()
        {
            return controlCenter.exoPlanet.Weather;
        }
    }
}