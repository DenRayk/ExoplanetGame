using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.RobotResults;

namespace ExoplanetGame.Presentation.Commands.Robot
{
    internal class CrashCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private IRobot robot;

        public CrashCommand(IRobot robot, UCCollection ucCollection)
        {
            this.robot = robot;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            RobotResult = ucCollection.UcCollectionRobot.CrashRobotService.Crash(robot);

            if (RobotResult.IsSuccess)
            {
                Console.WriteLine($"{robot.GetLanderName()} crashed.");
            }
            else
            {
                Console.WriteLine($"{RobotResult.Message}");
            }
        }
    }
}