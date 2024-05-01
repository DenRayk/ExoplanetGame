using ExoplanetGame.Application;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class LandCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;
        private IRobotRepository robotRepository;

        public LandCommand(RobotBase robotBase, UCCollection ucCollection, ExoplanetService exoplanetService, BaseCommand previousCommand)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
            robotRepository = RobotRepository.GetInstance();
        }

        public override void Execute()
        {
            PlanetMap planetMap = ucCollection.UcCollectionControlCenter.GetPlanetMapUseCase.GetPlanetMap();

            Position position = SelectLandPosition(planetMap);

            PositionResult positionResult = ucCollection.UcCollectionRobot.RobotLandService.LandRobot(robotBase, position);
            RobotResult = positionResult;

            if (RobotResult.IsSuccess)
            {
                if (RobotResult.Message != null)
                {
                    Console.WriteLine(positionResult.Message);
                }
                Console.WriteLine($"Robot landed on {positionResult.Position}");
                robotRepository.MoveRobot(robotBase, positionResult.Position);

                SelectRobotActionCommand selectRobotActionCommand = new SelectRobotActionCommand(ucCollection, robotBase, this);
                selectRobotActionCommand.Execute();
            }
            else
            {
                Console.WriteLine($"{positionResult.Message}");
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