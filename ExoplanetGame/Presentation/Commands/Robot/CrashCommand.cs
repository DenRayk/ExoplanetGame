using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class CrashCommand : RobotCommand
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
            RobotResult = ucCollection.UcCollectionRobot.CrashRobotService.Crash(robotBase);

            if (RobotResult.IsSuccess)
            {
                Console.WriteLine($"{robotBase.GetLanderName()} crashed.");
            }
            else
            {
                Console.WriteLine($"{RobotResult.Message}");
            }
        }
    }
}