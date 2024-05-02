using ExoplanetGame.Application;
using ExoplanetGame.Domain.ControlCenter;
using ExoplanetGame.Domain.Robot;
using ExoplanetGame.Domain.Robot.Variants;
using ExoplanetGame.Helper;

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
                Console.WriteLine($"{robotVariant.GetDescriptionFromEnum()} added successfully. \n");
            }
            catch (RobotCapacityReachException robotCapacityReachException)
            {
                Console.WriteLine(robotCapacityReachException.Message);
            }
        }
    }
}