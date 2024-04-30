using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Presentation.Commands.ControlCenter
{
    public class AddRobotCommand : BaseCommand
    {
        private RobotVariant robotVariant;

        public AddRobotCommand(RobotVariant robotVariant)
        {
            this.robotVariant = robotVariant;
        }

        public override void Execute()
        {
            ControlCenterCommand controlCenterCommand = new();
            controlCenterCommand.Execute();
        }
    }
}