using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class GetPositionCommand : RobotCommand
    {
        private readonly UCCollection ucCollection;
        private readonly IRobot robot;

        public GetPositionCommand(IRobot robot, UCCollection ucCollection)
        {
            this.robot = robot;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            try
            {
                PositionResult positionResult = PerformRobotPositioning();

                if (positionResult.IsSuccess)
                {
                    Console.WriteLine($"Robot is at {positionResult.Position}");
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

        private PositionResult PerformRobotPositioning()
        {
            PositionResult positionResult = ucCollection.UcCollectionRobot.GetPositionService.GetPosition(robot);
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

            Console.WriteLine("Cooling down the robot...");

            ucCollection.UcCollectionRobot.RobotCoolDownService.CoolDownRobot(robot, robot.RobotInformation.MaxHeat / 10);

            Console.Clear();
        }
    }
}