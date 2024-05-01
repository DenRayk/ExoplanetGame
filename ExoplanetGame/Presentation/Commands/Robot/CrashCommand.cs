using ExoplanetGame.Application;
using ExoplanetGame.Robot;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class CrashCommand : BaseCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;

        public CrashCommand(RobotBase robotBase, UCCollection ucCollection)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            throw new NotImplementedException();
        }
    }
}