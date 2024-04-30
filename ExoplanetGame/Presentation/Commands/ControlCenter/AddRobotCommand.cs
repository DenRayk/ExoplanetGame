using ExoplanetGame.Application;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Presentation.Commands.ControlCenter
{
    public class AddRobotCommand : BaseCommand
    {
        private UCCollection ucCollection;

        private RobotVariant robotVariant;

        public AddRobotCommand(RobotVariant robotVariant, UCCollection ucCollection)
        {
            this.robotVariant = robotVariant;
            this.ucCollection = ucCollection;
        }

        public override void Execute()
        {
            ucCollection.UcCollectionControlCenter.AddRobotUseCase.AddRobot(robotVariant);
            ControlCenterCommand controlCenterCommand = new(ucCollection);
            controlCenterCommand.Execute();
        }
    }
}