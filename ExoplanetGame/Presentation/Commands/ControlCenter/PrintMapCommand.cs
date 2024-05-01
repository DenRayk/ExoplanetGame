using ExoplanetGame.Application;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Helper;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;

namespace ExoplanetGame.Presentation.Commands.ControlCenter
{
    internal class PrintMapCommand : BaseCommand
    {
        private UCCollection ucCollection;

        public PrintMapCommand(UCCollection ucCollection, BaseCommand previousCommand) : base(previousCommand)
        {
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            PlanetMap planetMap = ucCollection.UcCollectionControlCenter.GetPlanetMapUseCase.GetPlanetMap();
            Dictionary<RobotBase, Position> robots = ucCollection.UcCollectionControlCenter.GetRobotsService.GetAllRobots();
            Weather weather = ucCollection.UcCollectionControlCenter.GetCurrentWeatherUseCase.GetCurrentWeather();

            Console.WriteLine($"Current weather: {weather.GetDescriptionFromEnum()}");
            Console.WriteLine($"Discovered area of the planet: {planetMap.GetPercentageOfExploredArea()}");
            MapPrinter.PrintMap(robots, planetMap);

            previousCommand.Execute();
        }
    }
}