using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGame.Application.ControlCenter
{
    internal class GetCurrentWeatherService : GetCurrentWeatherUseCase
    {
        private Domain.ControlCenter.ControlCenter controlCenter;

        public GetCurrentWeatherService()
        {
            this.controlCenter = Domain.ControlCenter.ControlCenter.GetInstance();
        }

        public Weather GetCurrentWeather()
        {
            return controlCenter.exoPlanet.Weather;
        }
    }
}