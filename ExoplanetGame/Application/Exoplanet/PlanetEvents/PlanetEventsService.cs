using ExoplanetGame.Domain.Exoplanet.Variants;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Application.Exoplanet.PlanetEvents
{
    public class PlanetEventsService
    {
        private readonly IExoplanetService exoplanetService;
        public ChangeWeatherUseCase ChangeWeatherService { get; }
        public FreezeUseCase FreezeService { get; }

        public RandomAttackUseCase RandomAttackService { get; }

        public VolcanicEruptionUseCase VolcanicEruptionService { get; }

        public PlanetEventsService(IExoplanetService exoplanetService, ChangeWeatherUseCase changeWeatherService, FreezeUseCase freezeService, RandomAttackUseCase randomAttackService, VolcanicEruptionUseCase volcanicEruptionService)
        {
            this.exoplanetService = exoplanetService;
            ChangeWeatherService = changeWeatherService;
            FreezeService = freezeService;
            RandomAttackService = randomAttackService;
            VolcanicEruptionService = volcanicEruptionService;
        }

        public RobotResultBase ExecutePlanetEvents(IRobot robot)
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