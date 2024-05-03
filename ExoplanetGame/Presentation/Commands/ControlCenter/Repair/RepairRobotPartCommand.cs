using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Helper;

namespace ExoplanetGame.Presentation.Commands.ControlCenter.Repair
{
    internal class RepairRobotPartCommand : BaseCommand
    {
        private UCCollection ucCollection;
        private IRobot robot;
        private RobotPart robotPart;

        public RepairRobotPartCommand(IRobot robot, RobotPart robotPart, UCCollection ucCollection)
        {
            this.robot = robot;
            this.robotPart = robotPart;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            ucCollection.UcCollectionRobot.RobotPartsHealthService.RepairRobotPart(robot, robotPart);

            Console.WriteLine($"Repaired {robotPart.GetDescriptionFromEnum()} on {robot.GetLanderName()} \n");
        }
    }
}