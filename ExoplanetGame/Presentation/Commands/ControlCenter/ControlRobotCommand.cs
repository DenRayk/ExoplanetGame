using ExoplanetGame.Application;
using ExoplanetGame.Presentation.Commands.PlanetSelection;
using ExoplanetGame.Presentation.Commands.Robot;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.Movement;

namespace ExoplanetGame.Presentation.Commands.ControlCenter
{
    public class ControlRobotCommand : BaseCommand
    {
        private UCCollection ucCollection;

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
                Console.WriteLine("No robots to control.");
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

            foreach (KeyValuePair<RobotBase, Position> robot in ucCollection.UcCollectionControlCenter.GetRobotsService.GetAllRobots())
            {
                if (robot.Key.HasLanded())
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