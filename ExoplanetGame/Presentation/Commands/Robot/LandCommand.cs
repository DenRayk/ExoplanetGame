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
        private readonly UCCollection ucCollection;
        private readonly IRobot robot;
        private readonly IRobotRepository robotRepository;

        public LandCommand(IRobot robot, UCCollection ucCollection)
        {
            this.robot = robot;
            this.ucCollection = ucCollection;
            robotRepository = RobotRepository.GetInstance();
        }

        public override void Execute()
        {
            try
            {
                PlanetMap planetMap = GetPlanetMap();
                Position position = SelectLandPosition(planetMap);

                PositionResult positionResult = LandRobot(position);
                RobotResult = positionResult;

                if (RobotResult.IsSuccess)
                {
                    if (RobotResult.Message != null)
                    {
                        Console.WriteLine(RobotResult.Message);
                    }
                    Console.WriteLine($"Robot landed on {positionResult.Position}");
                    robotRepository.MoveRobot(robot, positionResult.Position);

                    ExecuteRobotActionCommand();
                }
                else
                {
                    Console.WriteLine($"{positionResult.Message}");
                }
            }
            catch (RobotOverheatException exception)
            {
                HandleRobotOverheatException(exception);
            }
        }

        private PlanetMap GetPlanetMap()
        {
            return ucCollection.UcCollectionControlCenter.GetPlanetMapUseCase.GetPlanetMap();
        }

        private PositionResult LandRobot(Position position)
        {
            PositionResult positionResult = ucCollection.UcCollectionRobot.RobotLandService.LandRobot(robot, position);
            return positionResult;
        }

        private void ExecuteRobotActionCommand()
        {
            SelectRobotActionCommand selectRobotActionCommand = new SelectRobotActionCommand(ucCollection, robot);
            selectRobotActionCommand.Execute();
        }

        private Position SelectLandPosition(PlanetMap planetMap)
        {
            Console.WriteLine("Enter the X coordinate:");
            int x = GetMenuSelection(0, planetMap.PlanetSize.Width - 1);

            Console.WriteLine("Enter the Y coordinate:");
            int y = GetMenuSelection(0, planetMap.PlanetSize.Height - 1);

            return new Position(x, y);
        }

        private void HandleRobotOverheatException(RobotOverheatException exception)
        {
            Console.WriteLine(exception.Message);

            RobotResult = new PositionResult
            {
                IsSuccess = false,
                HasRobotSurvived = true,
                Message = exception.Message
            };

            ucCollection.UcCollectionRobot.RobotCoolDownService.CoolDownRobot(robot, robot.RobotInformation.MaxHeat / 10);
        }

    }
}