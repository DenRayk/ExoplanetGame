using ExoplanetGame.Application;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Helper;

namespace ExoplanetGame.Presentation.Commands.ControlCenter.Repair
{
    internal class RepairRobotPartCommand : RobotCommand
    {
        private UCCollection ucCollection;
        private RobotBase robotBase;
        private RobotPart robotPart;
        private ControlCenterCommand controlCenterCommand;

        public RepairRobotPartCommand(BaseCommand previousCommand, ControlCenterCommand controlCenterCommand, RobotBase robotBase, RobotPart robotPart, UCCollection ucCollection) : base(previousCommand, controlCenterCommand)
        {
            this.robotBase = robotBase;
            this.robotPart = robotPart;
            this.ucCollection = ucCollection;
            this.controlCenterCommand = controlCenterCommand;
        }

        public override void Execute()
        {
            ucCollection.UcCollectionRobot.RobotPartsHealthService.RepairRobotPart(robotBase, robotPart);

            Console.WriteLine($"Repaired {robotPart.GetDescriptionFromEnum()} on {robotBase.GetLanderName()} \n");

            controlCenterCommand.Execute();
        }
    }
}