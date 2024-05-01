using ExoplanetGame.Application;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Exoplanet.Environment;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Helper;
using ExoplanetGame.Presentation.Commands.ControlCenter;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class SelectRobotActionCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;
        private ControlCenterCommand controlCenterCommand;

        private readonly string helpText =
            "Robot Menu Information\n" +
            "Position:\t Show current position of the robot\n" +
            "Scan:\t\t Scan the environment\n" +
            "Move:\t\t Move the robot in the direction it is facing\n" +
            "Rotate:\t\t Rotate the robot left or right\n" +
            "Load:\t\t Load energy to the robot\n" +
            "Crash:\t\t Crash the robot\n";

        public SelectRobotActionCommand(UCCollection ucCollection, RobotBase robotBase, BaseCommand previousCommand, ControlCenterCommand controlCenterCommand) : base(previousCommand, controlCenterCommand)
        {
            this.ucCollection = ucCollection;
            this.robotBase = robotBase;
            this.controlCenterCommand = controlCenterCommand;
        }

        public override void Execute()
        {
            Console.WriteLine($"{robotBase.GetLanderName()} Menu");

            PlanetMap planetMap = ucCollection.UcCollectionControlCenter.GetPlanetMapUseCase.GetPlanetMap();
            Dictionary<RobotBase, Position> robots = ucCollection.UcCollectionControlCenter.GetRobotsService.GetAllRobots();
            Weather weather = ucCollection.UcCollectionControlCenter.GetCurrentWeatherUseCase.GetCurrentWeather();

            Console.WriteLine($"Current weather: {weather.GetDescriptionFromEnum()}");
            Console.WriteLine($"Discovered area of the planet: {planetMap.GetPercentageOfExploredArea()}");
            MapPrinter.PrintMap(robots, planetMap);

            var options = GetRobotMenuOptions();

            BaseCommand baseCommand = ReadUserInputWithOptions(options);
            if (baseCommand is HelpCommand helpCommand)
            {
                helpCommand.HelpText = helpText;
                helpCommand.Execute();
            }
            baseCommand.Execute();
        }

        private Dictionary<string, BaseCommand> GetRobotMenuOptions()
        {
            var options = new Dictionary<string, BaseCommand>
            {
                { "Position", new GetPositionCommand(robotBase, ucCollection, this, controlCenterCommand) },
                { "Scan", new ScanCommand(robotBase, ucCollection, this, controlCenterCommand) },
                { "Move", new MoveCommand(robotBase, ucCollection, this, controlCenterCommand) },
                { "Rotate", new ShowRotationOptionsCommand(robotBase, ucCollection, this, controlCenterCommand) },
                { "Load", new LoadCommand(robotBase, ucCollection, this, controlCenterCommand) },
                { "Crash", new CrashCommand(robotBase, ucCollection, this, controlCenterCommand) },
                { "Back", new ControlCenterCommand(ucCollection, this) }
            };

            return options;
        }
    }
}