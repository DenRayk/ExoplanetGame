using ExoplanetGame.Application;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Helper;

namespace ExoplanetGame.Presentation.Commands.ControlCenter
{
    internal class PrintMapCommand : BaseCommand
    {
        private UCCollection ucCollection;

        public PrintMapCommand(UCCollection ucCollection)
        {
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            PlanetMap planetMap = ucCollection.UcCollectionControlCenter.GetPlanetMapUseCase.GetPlanetMap();
            Dictionary<IRobot, Position> robots = ucCollection.UcCollectionControlCenter.GetRobotsService.GetAllRobots();
            Weather weather = ucCollection.UcCollectionControlCenter.GetCurrentWeatherUseCase.GetCurrentWeather();

            Console.WriteLine($"Current weather: {weather.GetDescriptionFromEnum()}");
            Console.WriteLine($"Discovered area of the planet: {planetMap.GetPercentageOfExploredArea()}");
            MapPrinter.PrintMap(robots, planetMap);
        }
    }
}