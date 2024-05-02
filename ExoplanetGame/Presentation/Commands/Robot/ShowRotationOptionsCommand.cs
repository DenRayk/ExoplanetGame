using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Movement;
using ExoplanetGame.Helper;
using ExoplanetGame.Presentation.Commands.ControlCenter;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class ShowRotationOptionsCommand : RobotCommand
    {
        private RobotBase robotBase;
        private UCCollection ucCollection;

        public ShowRotationOptionsCommand(RobotBase robotBase, UCCollection ucCollection)
        {
            this.robotBase = robotBase;
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
                options.Add(rotation.GetDescriptionFromEnum(), new RotateCommand(robotBase, ucCollection, rotation));
            }
            return options;
        }
    }
}