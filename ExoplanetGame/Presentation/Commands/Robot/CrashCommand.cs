using ExoplanetGame.Application;
using ExoplanetGame.Presentation.Commands.ControlCenter;
using ExoplanetGame.Robot;
using ExoplanetGame.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class CrashCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;
        private ControlCenterCommand controlCenterCommand;

        public CrashCommand(RobotBase robotBase, UCCollection ucCollection, BaseCommand previousCommand, ControlCenterCommand controlCenterCommand) : base(previousCommand, controlCenterCommand)
        {
            this.robotBase = robotBase;
            this.ucCollection = ucCollection;
            this.controlCenterCommand = controlCenterCommand;
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

            controlCenterCommand.Execute();
        }
    }
}