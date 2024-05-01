using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExoplanetGame.Application;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Presentation.Commands.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class LandCommand : BaseCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;
        private IRobotRepository robotRepository;

        public LandCommand(RobotBase robotBase, UCCollection ucCollection, ExoplanetService exoplanetService)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
            this.robotRepository = RobotRepository.GetInstance();
        }

        public override void Execute()
        {
            PlanetMap planetMap = ucCollection.UcCollectionControlCenter.GetPlanetMapUseCase.GetPlanetMap();

            Position position = SelectLandPosition(planetMap);

            PositionResult landResult = ucCollection.UcCollectionRobot.RobotLandService.LandRobot(robotBase, position);

            if (landResult.IsSuccess)
            {
                Console.WriteLine($"Robot landed on {landResult.Position}");
                robotRepository.MoveRobot(robotBase, landResult.Position);
                SelectRobotActionCommand selectRobotActionCommand = new(ucCollection, robotBase);
                selectRobotActionCommand.Execute();
            }
            else
            {
                Console.WriteLine($"{landResult.Message}");
                ControlCenterCommand controlCenterCommand = new(ucCollection);
                controlCenterCommand.Execute();
            }
        }

        private Position SelectLandPosition(PlanetMap planetMap)
        {
            Console.WriteLine("Enter the X coordinate:");
            int x = GetMenuSelection(planetMap.PlanetSize.Width - 1);

            Console.WriteLine("Enter the Y coordinate:");
            int y = GetMenuSelection(planetMap.PlanetSize.Height - 1);

            return new Position(x, y);
        }
    }
}