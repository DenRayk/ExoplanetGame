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