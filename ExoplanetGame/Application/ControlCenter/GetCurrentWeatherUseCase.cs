using ExoplanetGame.Domain.Exoplanet.Environment;

namespace ExoplanetGame.Application.ControlCenter
{
    public interface GetCurrentWeatherUseCase
    {
        public Weather GetCurrentWeather();
    }
}