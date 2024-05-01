using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGame.Application.ControlCenter
{
    public interface GetCurrentWeatherUseCase
    {
        public Weather GetCurrentWeather();
    }
}