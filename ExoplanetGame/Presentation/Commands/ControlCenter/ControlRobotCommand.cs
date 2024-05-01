using ExoplanetGame.Application;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Presentation.Commands.Robot;

namespace ExoplanetGame.Presentation.Commands.ControlCenter
{
    public class ControlRobotCommand : BaseCommand
    {
        private UCCollection ucCollection;
        private ExoplanetService exoplanetService;

        public ControlRobotCommand(UCCollection ucCollection, BaseCommand previousCommand) : base(previousCommand)
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
                ControlCenterCommand controlCenterCommand = new(ucCollection, this);
                controlCenterCommand.Execute();
            }

            Console.WriteLine("Select robot to control:");
            BaseCommand baseCommand = ReadUserInputWithOptions(robotsToControl);

            baseCommand.Execute();
        }

        private Dictionary<string, BaseCommand> GetRobotsToControl()
        {
            Dictionary<string, BaseCommand> robotsToControl = new();

            foreach (KeyValuePair<RobotBase, Position> robot in ucCollection.UcCollectionControlCenter.GetRobotsService.GetAllRobots())
            {
                if (robot.Key.HasLanded())
                {
                    robotsToControl.Add($"{robot.Key.GetLanderName()}", new SelectRobotActionCommand(ucCollection, robot.Key, this, (ControlCenterCommand)previousCommand));
                }
                else
                {
                    robotsToControl.Add($"{robot.Key.GetLanderName()}", new SelectRobotLandCommand(ucCollection, robot.Key, exoplanetService, this, (ControlCenterCommand)previousCommand));
                }
            }

            return robotsToControl;
        }
    }
}