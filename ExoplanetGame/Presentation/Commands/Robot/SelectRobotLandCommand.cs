using ExoplanetGame.Application;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Exoplanet.Environment;
using ExoplanetGame.Helper;
using ExoplanetGame.Presentation.Commands.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class SelectRobotLandCommand : BaseCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;
        private ExoplanetService exoplanetService;

        private string helpText =
            "Land:\t Land the robot on the planet\n" +
            "Back:\t Return to the control center\n";

        public SelectRobotLandCommand(UCCollection ucCollection, RobotBase robotBase, ExoplanetService exoplanetService)
        {
            this.ucCollection = ucCollection;
            this.robotBase = robotBase;
            this.exoplanetService = exoplanetService;
        }

        public override void Execute()
        {
            PlanetMap planetMap = ucCollection.UcCollectionControlCenter.GetPlanetMapUseCase.GetPlanetMap();
            Dictionary<RobotBase, Position> robots = ucCollection.UcCollectionControlCenter.GetRobotsService.GetAllRobots();
            Weather weather = ucCollection.UcCollectionControlCenter.GetCurrentWeatherUseCase.GetCurrentWeather();

            Console.WriteLine($"Current weather: {weather.GetDescriptionFromEnum()}");
            Console.WriteLine($"Discovered area of the planet: {planetMap.GetPercentageOfExploredArea()}");
            MapPrinter.PrintMap(robots, planetMap);

            Console.WriteLine("Pre-Landing Options (press F1 for help):");
            var options = getLandOptions();

            BaseCommand baseCommand;
            do
            {
                baseCommand = ReadUserInputWithOptions(options);

                if (baseCommand is HelpCommand helpCommand)
                {
                    helpCommand.HelpText = helpText;
                    helpCommand.PreviousCommand = this;
                    helpCommand.Execute();
                }
            } while (baseCommand is HelpCommand);

            baseCommand.Execute();
        }

        private Dictionary<string, BaseCommand> getLandOptions()
        {
            var options = new Dictionary<string, BaseCommand>();
            options.Add("Land", new LandCommand(robotBase, ucCollection, exoplanetService));
            options.Add("Back", new ControlCenterCommand(ucCollection));
            return options;
        }
    }
}