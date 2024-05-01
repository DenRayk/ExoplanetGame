using ExoplanetGame.Application;
using ExoplanetGame.Application.Exoplanet;
using ExoplanetGame.ControlCenter;
using ExoplanetGame.Robot.Variants;

namespace ExoplanetGame.Presentation.Commands.ControlCenter
{
    public class AddRobotCommand : BaseCommand
    {
        private UCCollection ucCollection;

        private RobotVariant robotVariant;

        public AddRobotCommand(RobotVariant robotVariant, UCCollection ucCollection, BaseCommand previousCommand) : base(previousCommand)
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

            previousCommand.Execute();
        }
    }
}