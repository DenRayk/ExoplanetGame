using ExoplanetGame.Application;

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
        }
    }
}