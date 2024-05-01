using ExoplanetGame.Application;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;
using ExoplanetGame.Helper;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class SelectRobotActionCommand : BaseCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;

        private readonly string helpText =
            "Robot Menu Information\n" +
            "Position:\t Show current position of the robot\n" +
            "Scan:\t\t Scan the environment\n" +
            "Move:\t\t Move the robot in the direction it is facing\n" +
            "Rotate:\t\t Rotate the robot left or right\n" +
            "Load:\t\t Load energy to the robot\n" +
            "Crash:\t\t Crash the robot\n";

        public SelectRobotActionCommand(UCCollection ucCollection, RobotBase robotBase, BaseCommand previousCommand)
        {
            this.ucCollection = ucCollection;
            this.robotBase = robotBase;
        }

        public override void Execute()
        {
            BaseCommand baseCommand;
            do
            {
                Console.WriteLine($"{robotBase.GetLanderName()} Menu");

                PlanetMap planetMap = ucCollection.UcCollectionControlCenter.GetPlanetMapUseCase.GetPlanetMap();
                Dictionary<RobotBase, Position> robots = ucCollection.UcCollectionControlCenter.GetRobotsService.GetAllRobots();
                Weather weather = ucCollection.UcCollectionControlCenter.GetCurrentWeatherUseCase.GetCurrentWeather();

                Console.WriteLine($"Current weather: {weather.GetDescriptionFromEnum()}");
                Console.WriteLine($"Discovered area of the planet: {planetMap.GetPercentageOfExploredArea()}");
                MapPrinter.PrintMap(robots, planetMap);

                var options = GetRobotMenuOptions();

                baseCommand = ReadUserInputWithOptions(options);

                if (baseCommand is HelpCommand helpCommand)
                {
                    helpCommand.HelpText = helpText;
                    helpCommand.Execute();
                }
                baseCommand.Execute();
            } while (baseCommand is not CrashCommand && baseCommand is not BackCommand && HasRobotSurvived(baseCommand));
        }

        private bool HasRobotSurvived(BaseCommand baseCommand)
        {
            if (baseCommand is RobotCommand robotCommand)
            {
                if (!robotCommand.RobotResult.HasRobotSurvived)
                {
                    return false;
                }
            }
            return true;
        }

        private Dictionary<string, BaseCommand> GetRobotMenuOptions()
        {
            var options = new Dictionary<string, BaseCommand>
            {
                { "Position", new GetPositionCommand(robotBase, ucCollection) },
                { "Scan", new ScanCommand(robotBase, ucCollection) },
                { "Move", new MoveCommand(robotBase, ucCollection) },
                { "Rotate", new ShowRotationOptionsCommand(robotBase, ucCollection) },
                { "Load", new LoadCommand(robotBase, ucCollection) },
                { "Crash", new CrashCommand(robotBase, ucCollection) },
                { "Back", new BackCommand(this) }
            };

            return options;
        }
    }
}