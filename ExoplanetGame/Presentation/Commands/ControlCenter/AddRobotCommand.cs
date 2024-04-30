using ExoplanetGame.Application;
using ExoplanetGame.ControlCenter;
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
            try
            {
                ucCollection.UcCollectionControlCenter.AddRobotUseCase.AddRobot(robotVariant);
            }
            catch (RobotCapacityReachException robotCapacityReachException)
            {
                Console.WriteLine(robotCapacityReachException.Message);
            }

            ControlCenterCommand controlCenterCommand = new(ucCollection);
            controlCenterCommand.Execute();
        }
    }
}