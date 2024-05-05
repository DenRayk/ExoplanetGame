using ExoplanetGame.Application;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Presentation.Commands.Robot;

namespace ExoplanetGame.Presentation.Commands.ControlCenter
{
    public class ControlRobotCommand : BaseCommand
    {
        private readonly UCCollection ucCollection;

        public ControlRobotCommand(UCCollection ucCollection)
        {
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            Dictionary<string, BaseCommand> robotsToControl = GetRobotsToControl();

            if (robotsToControl.Count == 0)
            {
                Console.Clear();
                Console.WriteLine("No Robots to control.");
                ControlCenterCommand controlCenterCommand = new(ucCollection);
                controlCenterCommand.Execute();
            }

            Console.WriteLine("Select robot to control:");
            BaseCommand baseCommand = ReadUserInputWithOptions(robotsToControl);

            baseCommand.Execute();
        }

        private Dictionary<string, BaseCommand> GetRobotsToControl()
        {
            Dictionary<string, BaseCommand> robotsToControl = new();

            foreach (KeyValuePair<IRobot, Position> robot in ucCollection.UcCollectionControlCenter.GetRobotsService.GetAllRobots())
            {
                if (robot.Key.RobotInformation.HasLanded)
                {
                    robotsToControl.Add($"{robot.Key.GetLanderName()}", new SelectRobotActionCommand(ucCollection, robot.Key));
                }
                else
                {
                    robotsToControl.Add($"{robot.Key.GetLanderName()}", new SelectRobotLandCommand(ucCollection, robot.Key));
                }
            }

            return robotsToControl;
        }
    }
}