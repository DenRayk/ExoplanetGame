using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
    internal class SelectRobotActionCommand : BaseCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;

        private readonly string helpText =
            "RobotPositionManager Menu Information\n" +
            "Position:\t Show current position of the RobotPositionManager\n" +
            "Scan:\t\t Scan the environment\n" +
            "Move:\t\t Move the robot in the direction it is facing\n" +
            "Rotate:\t\t Rotate the robot left or right\n" +
            "Load:\t\t Load energy to the robot\n" +
            "Crash:\t\t Crash the robot\n";

        public SelectRobotActionCommand(UCCollection ucCollection, RobotBase robotBase)
        {
            this.ucCollection = ucCollection;
            this.robotBase = robotBase;
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
            baseCommand.Execute();
        }

        private Dictionary<string, BaseCommand> GetRobotMenuOptions()
        {
            var options = new Dictionary<string, BaseCommand>
            {
                { "Position", new GetPositionCommand(robotBase, ucCollection) },
                { "Scan", new ScanCommand(robotBase, ucCollection) },
                { "Move", new MoveCommand(robotBase, ucCollection) },
                { "Rotate", new RotateCommand(robotBase, ucCollection) },
                { "Load", new LoadCommand(robotBase, ucCollection) },
                { "Crash", new CrashCommand(robotBase, ucCollection) },
                { "Back", new ControlCenterCommand(ucCollection) }
            };

            return options;
        }
    }
}