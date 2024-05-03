using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Helper;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class ShowRotationOptionsCommand : RobotCommand
    {
        private IRobot robot;
        private UCCollection ucCollection;

        public ShowRotationOptionsCommand(IRobot robot, UCCollection ucCollection)
        {
            this.robot = robot;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            var options = getRotationOptions();

            BaseCommand baseCommand = ReadUserInputWithOptions(options);

            baseCommand.Execute();
            RobotResult = ((RobotCommand)baseCommand).RobotResult;
        }

        private Dictionary<string, BaseCommand> getRotationOptions()
        {
            Dictionary<string, BaseCommand> options = new();
            foreach (Rotation rotation in Enum.GetValues(typeof(Rotation)))
            {
                options.Add(rotation.GetDescriptionFromEnum(), new RotateCommand(robot, ucCollection, rotation));
            }
            return options;
        }
    }
}