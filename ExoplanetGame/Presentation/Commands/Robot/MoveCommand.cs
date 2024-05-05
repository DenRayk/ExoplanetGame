using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class MoveCommand : RobotCommand
    {
        private readonly UCCollection ucCollection;
        private readonly IRobot robot;

        public MoveCommand(IRobot robot, UCCollection ucCollection)
        {
            this.robot = robot;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            try
            {
                PositionResult positionResult = PerformRobotMovement();

                if (positionResult.IsSuccess)
                {
                    if (positionResult.Message != null)
                    {
                        Console.WriteLine(positionResult.Message);
                    }

                    Console.WriteLine($"Robot moved to {positionResult.Position}");
                }
                else
                {
                    Console.WriteLine(positionResult.Message);
                }
            }
            catch (RobotOverheatException exception)
            {
                HandleRobotOverheatException(exception);
            }
        }

        private PositionResult PerformRobotMovement()
        {
            PositionResult positionResult = ucCollection.UcCollectionRobot.MoveRobotService.Move(robot);
            RobotResult = positionResult;
            return positionResult;
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