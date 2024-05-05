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
        private readonly UCCollection ucCollection;
        private readonly IRobot robot;

        private readonly string helpText =
            "Robot Menu Information\n" +
            "Position:\t Show current position of the robot\n" +
            "Scan:\t\t Scan the environment\n" +
            "Move:\t\t Move the robot in the direction it is facing\n" +
            "Rotate:\t\t Rotate the robot left or right\n" +
            "Load:\t\t Load energy to the robot\n" +
            "Crash:\t\t Crash the robot\n";

        public SelectRobotActionCommand(UCCollection ucCollection, IRobot robot)
        {
            this.ucCollection = ucCollection;
            this.robot = robot;
        }

        public override void Execute()
        {
            BaseCommand baseCommand;
            do
            {
                Console.WriteLine($"{robot.GetLanderName()} Menu");

                PlanetMap planetMap = ucCollection.UcCollectionControlCenter.GetPlanetMapUseCase.GetPlanetMap();
                Dictionary<IRobot, Position> robots = ucCollection.UcCollectionControlCenter.GetRobotsService.GetAllRobots();
                Weather weather = ucCollection.UcCollectionControlCenter.GetCurrentWeatherUseCase.GetCurrentWeather();

                Console.WriteLine($"Current weather: {weather.GetDescriptionFromEnum()}");
                Console.WriteLine($"Discovered area of the planet: {ucCollection.UcCollectionRobot.PlanetMapService.GetPercentageOfExploredArea()}");
                MapPrinter.PrintMap(robots, planetMap);

                var options = GetRobotMenuOptions();

                baseCommand = ReadUserInputWithOptions(options);

                if (baseCommand is HelpCommand helpCommand)
                {
                    helpCommand.HelpText = helpText;
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
                { "Position", new GetPositionCommand(robot, ucCollection) },
                { "Scan", new ScanCommand(robot, ucCollection) },
                { "Move", new MoveCommand(robot, ucCollection) },
                { "Rotate", new ShowRotationOptionsCommand(robot, ucCollection) },
                { "Load", new LoadCommand(robot, ucCollection) },
                { "Crash", new CrashCommand(robot, ucCollection) },
                { "Back", new BackCommand() }
            };

            return options;
        }
    }
}