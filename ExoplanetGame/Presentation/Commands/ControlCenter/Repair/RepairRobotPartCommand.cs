using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Helper;

namespace ExoplanetGame.Presentation.Commands.ControlCenter.Repair
{
    internal class RepairRobotPartCommand : BaseCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;
        private RobotPart robotPart;

        public RepairRobotPartCommand(RobotBase robotBase, RobotPart robotPart, UCCollection ucCollection)
        {
            this.robotBase = robotBase;
            this.robotPart = robotPart;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            ucCollection.UcCollectionRobot.RobotPartsHealthService.RepairRobotPart(robotBase, robotPart);

            Console.WriteLine($"Repaired {robotPart.GetDescriptionFromEnum()} on {robotBase.GetLanderName()} \n");
        }
    }
}