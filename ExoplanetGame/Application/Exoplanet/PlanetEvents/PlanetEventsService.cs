using ExoplanetGame.Domain.Exoplanet.Variants;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet.PlanetEvents
{
    public class PlanetEventsService
    {
        private ExoplanetService exoplanetService;
        public ChangeWeatherUseCase ChangeWeatherService { get; }
        public FreezeUseCase FreezeService { get; }

        public RandomAttackUseCase RandomAttackService { get; }

        public VolcanicEruptionUseCase VolcanicEruptionService { get; }

        public PlanetEventsService(ExoplanetService exoplanetService)
        {
            this.exoplanetService = exoplanetService;
            ChangeWeatherService = new ChangeWeatherService(exoplanetService);
            FreezeService = new FreezeService(exoplanetService);
            RandomAttackService = new RandomAttackService();
            VolcanicEruptionService = new VolcanicEruptionService();
        }

        public RobotResultBase ExecutePlanetEvents(RobotBase robot)
        {
            RobotResultBase robotResult = new RobotResultBase()
            {
                IsSuccess = true,
                HasRobotSurvived = true
            };

            if (exoplanetService.ExoPlanet is Frostfell)
            {
                robotResult = FreezeService.FreezeRobotIfItHasntMovedForAWhile(robot);
            }
            else if (exoplanetService.ExoPlanet is Lavaria)
            {
                robotResult = VolcanicEruptionService.HandleVolcanicEruption(robot);
            }
            else if (exoplanetService.ExoPlanet is Tropica)
            {
                robotResult = RandomAttackService.HandleMysteriousAttack(robot);
            }

            ChangeWeatherService.ChangeWeather();

            return robotResult;
        }
    }
}