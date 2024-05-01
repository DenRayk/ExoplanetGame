using ExoplanetGame.Application;
using ExoplanetGame.Presentation.Commands.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.RobotResults;

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
            RobotResultBase robotResult = ucCollection.UcCollectionRobot.CrashRobotService.Crash(robotBase);

            if (robotResult.IsSuccess)
            {
                Console.WriteLine($"{robotBase.GetLanderName()} crashed.");
            }
            else
            {
                Console.WriteLine($"{robotResult.Message}");
            }

            ControlCenterCommand controlCenterCommand = new(ucCollection);
            controlCenterCommand.Execute();
        }
    }
}