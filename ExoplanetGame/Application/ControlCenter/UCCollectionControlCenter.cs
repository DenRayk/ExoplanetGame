namespace ExoplanetGame.Application.ControlCenter
{
    public class UCCollectionControlCenter
    {
        public UCCollectionControlCenter()
        {
            SelectPlanetUseCase = new SelectPlanetService();
            AddRobotUseCase = new AddRobotService();
            GetRobotsService = new GetRobotsService();
            GetPlanetMapUseCase = new GetPlanetMapService();
            GetCurrentWeatherUseCase = new GetCurrentWeatherService();
        }

        public SelectPlanetUseCase SelectPlanetUseCase { get; }
        public AddRobotUseCase AddRobotUseCase { get; }
        public GetRobotsService GetRobotsService { get; }

        public GetCurrentWeatherUseCase GetCurrentWeatherUseCase { get; }

        public GetPlanetMapUseCase GetPlanetMapUseCase { get; }
    }
}