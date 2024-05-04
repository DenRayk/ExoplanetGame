using ExoplanetGame.Application;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    public class LandCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private IRobot robot;
        private IRobotRepository robotRepository;

        public LandCommand(IRobot robot, UCCollection ucCollection)
        {
            this.robot = robot;
            this.ucCollection = ucCollection;
            robotRepository = RobotRepository.GetInstance();
        }

        public override void Execute()
        {
            PlanetMap planetMap = ucCollection.UcCollectionControlCenter.GetPlanetMapUseCase.GetPlanetMap();

            Position position = SelectLandPosition(planetMap);

            PositionResult positionResult = ucCollection.UcCollectionRobot.RobotLandService.LandRobot(robot, position);
            RobotResult = positionResult;

            if (RobotResult.IsSuccess)
            {
                if (RobotResult.Message != null)
                {
                    Console.WriteLine(positionResult.Message);
                }
                Console.WriteLine($"Robot landed on {positionResult.Position}");
                robotRepository.MoveRobot(robot, positionResult.Position);

                SelectRobotActionCommand selectRobotActionCommand = new SelectRobotActionCommand(ucCollection, robot);
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
            int x = GetMenuSelection(0, planetMap.PlanetSize.Width - 1);

            Console.WriteLine("Enter the Y coordinate:");
            int y = GetMenuSelection(0, planetMap.PlanetSize.Height - 1);

            return new Position(x, y);
        }
    }
}